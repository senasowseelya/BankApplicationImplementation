using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BankingApplication.Services;
using BankingApplication.Models;
using BankingApplication.Database;

namespace BankingApplication.Consl
{
    internal class UserActions
    {

        bool status = false;
        String currencyName;
        double amount = 0.0;
        Account userAccount;
        AccountService accountService = new AccountService();
        CommonFunctions commonFunctions = new CommonFunctions();

        internal UserActions()
        {   
            Credentials userCredentials = commonFunctions.GetCredentials();
            userAccount = GetActiveUserAccount(userCredentials);
            while (userAccount == null)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Invalid Username or Password");
                Console.WriteLine("Choose\n1.Login\n2.Exit ");
                Console.WriteLine("-----------------------------------------------");
                String userChoice = Console.ReadLine();
                if (userChoice == "2")
                    Environment.Exit(0);
                userCredentials = commonFunctions.GetCredentials();
                userAccount = GetActiveUserAccount(userCredentials);
            }
            while (true)
                {
                    Menu();
                    
                    try
                    {
                        switch ((EnumUserOptions)Convert.ToInt32(Console.ReadLine()))
                        {
                            case EnumUserOptions.Deposit:
                                {
                                    DepositHandler();
                                    break;
                                }
                            case EnumUserOptions.Withdraw:
                                {
                                    WithdrawHandler();
                                    break;
                                }
                            case EnumUserOptions.Transfer:
                                {
                                    TransferHandler();
                                    break;
                                }

                            case EnumUserOptions.DisplayTransactions:
                                {
                                    DisplayTransactionsHandler();
                                    break;
                                }
                            case EnumUserOptions.ChangePassword:
                                {
                                    ChangePasswordHandler();
                                    break;
                                }
                            case EnumUserOptions.Logout:
                                {
                                    Program.Main();
                                    break;
                                }
                            default:
                                Environment.Exit(0);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        private void ChangePasswordHandler()
        {
            Console.WriteLine("Enter New Password");
            String newPassword = Console.ReadLine();
            status=accountService.ChangePassword(userAccount,newPassword);
            commonFunctions.DisplayStatus(status, $"Succesfully updated password ");

        }

        private void DepositHandler()
        {
            status = false;
            amount = ReadAmount();
            if (amount>0)
            {
                currencyName = ReadCurrency();
                status = accountService.Deposit(userAccount, amount, currencyName);
                commonFunctions.DisplayStatus(status, $"Succesfully Deposited {amount} {currencyName}"); 
            }
            else
            {
                Console.WriteLine("You have entered Invalid Amount");
            }
        }
        private void WithdrawHandler()
        {
            status = false;
            amount = ReadAmount();
            if (amount>0)
            {
                currencyName = ReadCurrency();
                status = accountService.Withdraw(userAccount, amount, currencyName);
                commonFunctions.DisplayStatus(status, $"Succesfully WithDrawn {amount} {currencyName}"); 
            }
            else
            {
                Console.WriteLine("You have entered Invalid Amount");
            }
        }
        private void TransferHandler()
        {
            status = false;
            Console.WriteLine("Enter Receiver Account Number");
            String recAccNum = Console.ReadLine();
            amount = ReadAmount();
            if (amount>0)
            {
                Console.WriteLine("Choose Mode of Transfer\n1.RTGS\n2.IMPS\n3.exit");
                EnumModeOfTransfer mode = (EnumModeOfTransfer)Convert.ToInt32(Console.ReadLine());
                status = accountService.TransferAmount(userAccount, recAccNum, amount, mode);
                commonFunctions.DisplayStatus(status, $"Succesfully Transferred {amount}"); 
            }
            else
                Console.WriteLine(" You have entered Invalid Amount");
        }
        private void DisplayTransactionsHandler()
        {
            status = false;
            List<Transaction> Transactions = accountService.DisplayTransactions(userAccount);
            foreach (var Transaction in Transactions)
            {
                Console.WriteLine($" {Transaction.TransId} {Transaction.Type} {Transaction.Amount} {Transaction.CurrencyName}");
            }
        }
        private void Menu()
        {
            Console.WriteLine("----XYZ Banking Service----");
            Console.WriteLine("\n1.Deposit\n2.Withdrawl\n3.Transfer Money\n4.Display Transactions\n5.Change Password\n6.Logout\nOthers:Exit");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Enter Choice\n");
        }
        private double ReadAmount()
        {
            Console.WriteLine("Enter Amount");
            double amount = Convert.ToDouble(Console.ReadLine());
            return amount;
        }
        private string ReadCurrency()
        {

            Console.WriteLine("Enter Type of Currency");
            currencyName = Console.ReadLine();
            return currencyName;
        }
        private Account GetActiveUserAccount(Credentials UserCredentials)
        {
            foreach (Bank bank in BankData.banks)
                {
                    Account account = bank.Accounts.SingleOrDefault(acc => acc.UserName.Equals(UserCredentials.UserName) && acc.Password==UserCredentials.Password);
                    if (account != null && ValidateAccount(account))
                        return account;
                        
                }
            return null;
        }

        private bool ValidateAccount(Account account)
        {
            if (account.IsActive.Equals(true))
                return true;
            return false;
               
               
        }
    }
}

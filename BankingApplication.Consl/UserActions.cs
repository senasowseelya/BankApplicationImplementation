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
        internal UserActions()
        {
            Credentials userCredentials = new Commonfunctions().GetCredentials();
            userAccount = GetActiveUserAccount(userCredentials);
            while (userAccount == null)
            {
                Console.WriteLine("Invalid Username or Password");
                Console.WriteLine("Choose\n1.Login\n2.Exit ");
                String userChoice = Console.ReadLine();
                if (userChoice == "2")
                    Environment.Exit(0);
                userCredentials = new Commonfunctions().GetCredentials();
                userAccount = GetActiveUserAccount(userCredentials);
            }
            while (true)
                {
                    Menu();
                    EnumUserOptions Choice = (EnumUserOptions)Enum.Parse(typeof(EnumUserOptions), Console.ReadLine());
                    try
                    {
                        switch (Choice)
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
            new Commonfunctions().DisplayStatus(status, $"Succesfully updated password ");

        }

        private void DepositHandler()
        {
            status = false;
            amount = ReadAmount();
            currencyName = ReadCurrency();
            status = accountService.Deposit(userAccount, amount, currencyName);
            new Commonfunctions().DisplayStatus(status, $"Succesfully Deposited {amount}");
        }
        private void WithdrawHandler()
        {
            status = false;
            amount = ReadAmount();
            currencyName = ReadCurrency();
            status = accountService.Withdraw(userAccount, amount, currencyName);
            new Commonfunctions().DisplayStatus(status, $"Succesfully WithDrawn {amount}");
        }
        private void TransferHandler()
        {
            status = false;
            Console.WriteLine("Enter Receiver Account Number");
            String recAccNum = Console.ReadLine();
            amount = ReadAmount();
            currencyName = ReadCurrency();
            Console.WriteLine("Choose Mode of Transfer\n1.RTGS\n2.IMPS\n3.exit");
            EnumModeOfTransfer mode = (EnumModeOfTransfer)Enum.Parse(typeof(EnumModeOfTransfer), Console.ReadLine());
            status = accountService.TransferAmount(userAccount, recAccNum, amount, mode, currencyName);
            new Commonfunctions().DisplayStatus(status, $"Succesfully Transferred {amount}");
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
            Console.WriteLine("****XYZ Banking Service****");
            Console.WriteLine("\n1.Deposit\n2.Withdrawl\n3.Transfer Money\n4.Display Transactions\n5.Change Password\n6.Logout\nOthers:Exit");
            Console.WriteLine("***************************");
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
                foreach (Account Account in bank.Accounts)
                {
                    if (Account.UserName.Equals( UserCredentials.UserName) && Account.Password.Equals(UserCredentials.Password) )
                    {
                        if(ValidateAccount(Account))
                            return Account;

                    }
                }
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

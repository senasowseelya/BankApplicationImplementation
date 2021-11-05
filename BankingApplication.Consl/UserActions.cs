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


        
        String currencyName;
        double amount = 0.0;
        public Account userAccount { get; set; }
        public AccountService accountService { get; set; }
        Common commonFunctions = new Common();

        internal UserActions(Credentials userCredentials)
        {
            userAccount=GetActiveUserAccount(userCredentials);
            accountService = new AccountService();

        }
        public void UserActivities()
        { 
           while (true)
                {
                    Menu();
                    
                    try
                    {
                        switch ((UserOptions)Convert.ToInt32(Console.ReadLine()))
                        {
                            case UserOptions.Deposit:
                                {
                                    Deposit();
                                    break;
                                }
                            case UserOptions.Withdraw:
                                {
                                    Withdraw();
                                    break;
                                }
                            case UserOptions.Transfer:
                                {
                                    Transfer();
                                    break;
                                }

                            case UserOptions.DisplayTransactions:
                                {
                                    DisplayTransactions();
                                    break;
                                }
                            case UserOptions.ChangePassword:
                                {
                                    ChangePassword();
                                    break;
                                }
                            case UserOptions.Logout:
                                {
                                return;
                                }
                            default:
                                Console.WriteLine("Please choose from above options only");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        private void ChangePassword()
        {
            Console.WriteLine("Enter New Password");
            String newPassword = Console.ReadLine();
            
            commonFunctions.DisplayStatus(accountService.ChangePassword(userAccount, newPassword), $"Succesfully updated password ");

        }

        private void Deposit()
        {
            
            amount = ReadAmount();
            if (amount>0)
            {
                currencyName = ReadCurrency();
                commonFunctions.DisplayStatus(accountService.Deposit(userAccount, amount, currencyName),$"Succesfully Deposited {amount} {currencyName}"); 
            }
            else
            {
                Console.WriteLine("You have entered Invalid Amount");
            }
        }
        private void Withdraw()
        {
            
            amount = ReadAmount();
            if (amount>0)
            {
                currencyName = ReadCurrency();
                
                commonFunctions.DisplayStatus(accountService.Withdraw(userAccount, amount, currencyName), $"Succesfully WithDrawn {amount} {currencyName}"); 
            }
            else
            {
                Console.WriteLine("You have entered Invalid Amount");
            }
        }
        private void Transfer()
        {
            
            Console.WriteLine("Enter Receiver Account Number");
            String recAccNum = Console.ReadLine();
            amount = ReadAmount();
            if (amount>0)
            {
                Console.WriteLine("Choose Mode of Transfer\n1.RTGS\n2.IMPS\n3.exit");
                ModeOfTransfer mode = (ModeOfTransfer)Convert.ToInt32(Console.ReadLine());
                
                commonFunctions.DisplayStatus(accountService.TransferAmount(userAccount, recAccNum, amount, mode), $"Succesfully Transferred {amount}"); 
            }
            else
                Console.WriteLine(" You have entered Invalid Amount");
        }
        private void DisplayTransactions()
        {
            List<Transaction> Transactions = accountService.DisplayTransactions(userAccount);
            foreach (var Transaction in Transactions)
            {
                Console.WriteLine($" {Transaction.TransId} {Transaction.Type} {Transaction.Amount} {Transaction.CurrencyName}");
            }
        }
        private void Menu()
        {
            Console.WriteLine("----XYZ Banking Service----");
            Console.WriteLine("\n1.Deposit\n2.Withdrawl\n3.Transfer Money\n4.Display Transactions\n5.Change Password\n6.Logout\n");
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
                Account account = bank.Accounts.SingleOrDefault(acc => acc.User.UserName.Equals(UserCredentials.UserName) && acc.User.Password == UserCredentials.Password);
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

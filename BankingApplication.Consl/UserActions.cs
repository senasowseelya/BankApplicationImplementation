

using BankingApplication.Data;
using BankingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApplication.Consl
{
    internal class UserActions
    {
        
        public Common commonFunctions { get; set; }
        public AccountService accountService { get; set; }
        public UserActions()
        {
            this.accountService=new AccountService();
            this.commonFunctions = new Common();
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
           var  accNumber = ReadAccNumber();
            Console.WriteLine("Enter Current Password");
            string currentPassword = Console.ReadLine();
            Console.WriteLine("Enter New Password");
            string newPassword = Console.ReadLine();
            if(accountService.ChangePassword(accNumber,currentPassword,newPassword))
                Console.WriteLine("Password Chnaged succesfully");
            else
                Console.WriteLine("Invalid AccountNumber or Invalid Old Password");

            
        }

        private void DisplayTransactions()
        {
             var accNumber = ReadAccNumber();
            List<Transaction> transactions= accountService.Displaytransactions(accNumber);
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine(transaction.transid + " " + transaction.transactionAmount + " " + transaction.transactionOn + " " + transaction.type);
            }
        }

        private void Menu()
        {
            Console.WriteLine("----XYZ Banking Service----");
            Console.WriteLine("\n1.Deposit\n2.Withdrawl\n3.Transfer Money\n4.Display Transactions\n5.Change Password\n6.Logout\n");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Enter Choice\n");
        }
        private void Deposit()
        {
            var accNumber=ReadAccNumber();
            var amount = ReadAmount();
            string currencyName;
            if (amount > 0)
            {
                currencyName = ReadCurrency();
                commonFunctions.DisplayStatus(accountService.Deposit(accNumber,amount, currencyName), $"Succesfully Deposited {amount} {currencyName}");
            }
            else
            {
                Console.WriteLine("You have entered Invalid Amount");
            }
        }
        private string ReadAccNumber()
        {
            Console.WriteLine("Enter Account Number");
            string accNumber = Console.ReadLine();
            return accNumber;
        }
        private decimal ReadAmount()
        {
            Console.WriteLine("Enter Amount");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            return amount;
        }
        private string ReadCurrency()
        {

            Console.WriteLine("Enter Type of Currency");
             var currencyName = Console.ReadLine();
            return currencyName;
        }
        private void Withdraw()
        {
             var accNumber = ReadAccNumber();
             var amount = ReadAmount();
            string currencyName;
            if (amount>0)
            {
                currencyName = ReadCurrency();
                commonFunctions.DisplayStatus(accountService.Withdraw(accNumber, amount, currencyName), $"Succesfully withdrawn {amount} {currencyName}");
            }
            else
            {
                Console.WriteLine("You have entered Invalid Amount");
            }

        }
        private void Transfer()
        {
            var  accNumber = ReadAccNumber();
            Console.WriteLine("Enter Receiver Account Number");
            String recAccNum = Console.ReadLine();
             var amount = ReadAmount();
            string currencyName;
            currencyName = ReadCurrency();
            if (amount > 0)
            {
                Console.WriteLine("Choose Mode of Transfer\n1.RTGS\n2.IMPS\n3.exit");
                string mode =Console.ReadLine();
                commonFunctions.DisplayStatus(accountService.TransferAmount(accNumber, recAccNum, amount,currencyName, mode), $"Succesfully Transferred {amount}");
            }
            else
                Console.WriteLine(" You have entered Invalid Amount");
        }



    }
}
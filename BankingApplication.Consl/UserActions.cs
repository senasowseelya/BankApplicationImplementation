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
        string accountNumber;
        double amount = 0.0;
        bool isValidUser = false;
        AccountService accountService = new AccountService();
        internal UserActions()
        {
            var userCredentials = new Program().GetCredentials();
            isValidUser = new Program().ValidateAccount(userCredentials);
            if (!isValidUser)
            {
                Console.WriteLine("Invalid Username or Password ");
                new UserActions();
            }
            else
            {
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

        }
        private void DepositHandler()
        {
            status = false;
            accountNumber = ReadAccountNum();
            amount = ReadAmount();
            status = accountService.Deposit(accountNumber, amount);
            if (status)
            {
                Console.WriteLine("Succesfully Deposited {0}", amount);
            }

        }
        private void WithdrawHandler()
        {
            status = false;
            accountNumber = ReadAccountNum();
            amount = ReadAmount();
            status = accountService.Withdraw(accountNumber, amount);
            if (status)
            {
                Console.WriteLine("Succesfully Withdrawn {0}", amount);
            }

        }
        private void TransferHandler()
        {
            status = false;
            string sendAccNum =ReadAccountNum("From");
            string recAccNum = ReadAccountNum("To");
            amount = ReadAmount();
            Console.WriteLine("Choose Mode of Transfer\n1.RTGS\n2.IMPS\n3.exit");
            EnumModeOfTransfer mode = (EnumModeOfTransfer)Enum.Parse(typeof(EnumModeOfTransfer), Console.ReadLine());
            status = accountService.TransferAmount(sendAccNum, recAccNum,amount,mode);
            if (status)
            {
                Console.WriteLine("Succesfully Transferred {0}", amount);
            }
        }
        private void DisplayTransactionsHandler()
        {
            status = false;
            accountNumber = ReadAccountNum("");

            List<Transaction> Transactions = accountService.DisplayTransactions(accountNumber);
            foreach (var Transaction in Transactions)
            {
                Console.WriteLine(Transaction.TransId + " " + Transaction.Amount + " " + Transaction.Type);
            }


        }
        private void Menu()
        {
            Console.WriteLine("****XYZ Banking Service****");
            Console.WriteLine("\n1.Deposit\n2.Withdrawl\n3.Transfer Money\n4.Display Transactions\n5.Logout\nOthers:Exit");
            Console.WriteLine("***************************");
            Console.WriteLine("Enter Choice\n");
        }

        private String ReadAccountNum(string S = "")
        {
            Console.WriteLine("Enter {0} Account Number", S);
            String accNum = Console.ReadLine();
            return accNum;
        }
        private double ReadAmount()
        {
            Console.WriteLine("Enter Amount");
            double amount = Convert.ToDouble(Console.ReadLine());
            return amount;
        }
        public bool CheckBank(String BankName)
        {
            var banks = new JsonReadWrite().ReadData();
            if (!banks.Any(e => e.Name == BankName))
                throw new BankDoesntExistException();
            return true;
        }


    }
}

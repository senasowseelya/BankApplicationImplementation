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
            var userCredentials = new Program().GetCredentials();
            userAccount = new Program().GetUserAccount(userCredentials);
            if (userAccount==null)
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
            amount = ReadAmount();
            currencyName = ReadCurrency();
            status = accountService.Deposit(userAccount, amount, currencyName);
            if(status)
            {
                Console.WriteLine("Succesfully Deposited {0}", amount);
            }

        }

        private string ReadCurrency()
        {
            Console.WriteLine("Enter Type of Currency");
            currencyName = Console.ReadLine();
            return currencyName;
        }

        private void WithdrawHandler()
        {
            status = false;
            amount = ReadAmount();
            currencyName = ReadCurrency();
            status = accountService.Withdraw(userAccount, amount,currencyName);
            if (status)
            {
                Console.WriteLine("Succesfully Withdrawn {0}", amount);
            }

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
            if (status)
            {
                Console.WriteLine("Succesfully Transferred {0}", amount);
            }
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
            Console.WriteLine("\n1.Deposit\n2.Withdrawl\n3.Transfer Money\n4.Display Transactions\n5.Logout\nOthers:Exit");
            Console.WriteLine("***************************");
            Console.WriteLine("Enter Choice\n");
        }

      
        private double ReadAmount()
        {
            Console.WriteLine("Enter Amount");
            double amount = Convert.ToDouble(Console.ReadLine());
            return amount;
        }
        


    }
}

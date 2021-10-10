using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
namespace BankApp
{
    public class Program
    {
        enum ServicesAvailable
        {
            CreateAccount = 1,
            DepositAmount,
            WithDrawAmount,
            TransferAmount,
            DisplayTransactions
        }
        public static int Main()
        {
            bool status=false;
            string accNum;
            double amount;
            while (true)
            {
                new Program().HomePage();
                BankServices service = new BankServices();
                ServicesAvailable Choice = (ServicesAvailable)Enum.Parse(typeof(ServicesAvailable),Console.ReadLine());
                switch (Choice)
                {
                    case ServicesAvailable.CreateAccount:
                        {
                            Customer newAcc = new Customer();
                            Console.WriteLine("Enter Account Holder's Full Name:");
                            newAcc.accHoldName = Console.ReadLine();
                            Console.WriteLine("Enter Account Holder's Date of Birth:");
                            newAcc.dob = Console.ReadLine();
                            Console.WriteLine("Enter Account Holder's Aadhar Number:");
                            newAcc.aadhar = (Console.ReadLine());
                            Console.WriteLine("Enter Phone number:");
                            newAcc.phno = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Type(Savings/Current):");
                            newAcc.type = Console.ReadLine();
                            Console.WriteLine("Enter Gender:");
                            newAcc.gender = Console.ReadLine();
                            Console.WriteLine("Enter Age :");
                            newAcc.age = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter  Marital Status");
                            newAcc.maritalStatus = Console.ReadLine();
                            Console.WriteLine("Enter Father/Husband name:");
                            newAcc.fathOrHusbName = Console.ReadLine();
                            Console.WriteLine("Enter Address:");
                            Console.WriteLine("Enter town/village:");
                            newAcc.town = Console.ReadLine();
                            Console.WriteLine("Enter  District:");
                            newAcc.dist = Console.ReadLine();
                            Console.WriteLine("Enter state:");
                            newAcc.state = Console.ReadLine();
                            Console.WriteLine("Enter pin:");
                            newAcc.pin = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Nationality:");
                            newAcc.nationality = Console.ReadLine();
                            Console.WriteLine("Enter Religion:");
                            newAcc.religion = Console.ReadLine();
                            Console.WriteLine("Enter category:");
                            newAcc.category = Console.ReadLine();
                            newAcc.dateOfIssue = DateTime.Now.ToString();
                            newAcc.address = newAcc.town + "-" + newAcc.dist + "-" + newAcc.state + "-" + newAcc.pin;
                            var today = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "Created Account";
                            newAcc.trans.Add(today);

                            service.CreateAccountService( newAcc);
                            Console.WriteLine("Your Account Number is {0}",newAcc.accno);
                            break;
                        }
                    case ServicesAvailable.DepositAmount:
                        {
                            status = false;
                            accNum = new Program().ReadAccountNum("");
                            amount = new Program().ReadAmount();
                            try
                            {
                                status = service.DepositAmount(accNum,amount);
                            }
                            catch (AccountDoesntExistException ex)
                            {
                                Console.WriteLine("Accoutn doesn't exist . Please try with valid account.",ex.Message);
                            }
                            if (status)
                            {
                                Console.WriteLine("Succesfully Deposited {0}",amount);
                            }
                            break;
                        }
                            
                        
                    case ServicesAvailable.WithDrawAmount:
                        {
                            status = false;
                            accNum = new Program().ReadAccountNum("");
                            amount = new Program().ReadAmount();
                            try
                            {
                                status = service.WithdrawMoney(accNum, amount);
                            }
                            catch (AccountDoesntExistException ex)
                            {
                                Console.WriteLine("Account doesn't exist. Please try with valid account.",ex.Message);
                            }
                            catch(InsufficientAmountException ex)
                            {
                                Console.WriteLine("Insufficient Amount To withdraw", ex.Message);
                            }
                            if (status)
                            {
                                Console.WriteLine("Succesfully Withdrawn {0}", amount);
                            }
                            break;
                        }
                    case ServicesAvailable.TransferAmount:
                        {
                            status = false;
                            string SendAccNum = new Program().ReadAccountNum("From");
                            string RecAccNum = new Program().ReadAccountNum("To");
                            amount = new Program().ReadAmount();

                            try
                            {
                                 status= service.TransferAmount(SendAccNum, RecAccNum, amount);

                            }
                            catch (AccountDoesntExistException ex)
                            {
                                Console.WriteLine("Accoutn doesn't exist. Please try with valid account.", ex.Message);
                            }
                            catch (InsufficientAmountException ex)
                            {
                                Console.WriteLine("Insufficient Amount To withdraw", ex.Message);
                            }
                            if (status)
                            {
                                Console.WriteLine("Succesfully Transferred {0}", amount);
                            }
                            break;


                        }
                    case ServicesAvailable.DisplayTransactions:
                        {
                            status = false;
                            List <String> transactions;
                            accNum = new Program().ReadAccountNum("");
                            try
                            {
                                 transactions=service.DisplayTransactions(accNum) ;
                                foreach (string trans in transactions)
                                {
                                    Console.WriteLine(trans);
                                }
                                
                            }
                            catch (AccountDoesntExistException ex)
                            {
                                Console.WriteLine("Accoutn doesn't exist. Please try with valid account.", ex.Message);
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Exited....");
                            Environment.Exit(0);
                            break;
                        }
                }

            }
        }
        private void HomePage()
        {
            Console.WriteLine("****XYZ Banking Service****");
            Console.WriteLine("1.Create Account\n2.Deposit\n3.Withdrawl\n4.Transfer Money\n5.Display Transactions\nOthers:Exit");
            Console.WriteLine("***************************");
            Console.WriteLine("Enter Choice\n");
        }
       
        private String ReadAccountNum(string s = "")
        {
            Console.WriteLine("Enter {0} Account Number", s);
            String accNum = Console.ReadLine();
            return accNum;
        }
        private double ReadAmount()
        {
            Console.WriteLine("Enter Amount");
            double amount = Convert.ToDouble(Console.ReadLine());
            return amount;
        }
    }
}

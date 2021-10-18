using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
namespace BankApp
{
    public class Program
    {
        public static int Main()
        {
            bool status = false;
            string AccNum;
            double Amount = 0.0;
            while (true)
            {
                new Program().HomePage();
                BankService service = new BankService();
                EnumServicesAvailable Choice = (EnumServicesAvailable)Enum.Parse(typeof(EnumServicesAvailable), Console.ReadLine());
                switch (Choice)
                {
                    case EnumServicesAvailable.AddBank:
                        {
                            status = false;
                            Console.WriteLine("Enter Name of Bank: ");
                            String name = Console.ReadLine();
                            try
                            {
                                status = service.AddBank(name);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }
                            if (status)
                            {
                                Console.WriteLine("Succesfully  Added Bank ");
                            }
                            break;



                        }
                    case EnumServicesAvailable.CreateAccount:
                        {
                            Console.WriteLine("Enter Bank Name:");
                            String BankName = Console.ReadLine();
                            try
                            {
                                new BankService().CheckBank(BankName);
                            }
                            catch (BankDoesntExistException ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }
                            Account newAcc = new Account();
                            Console.WriteLine("Enter Account Holder's Full Name:");
                            newAcc.accHoldName = Console.ReadLine();
                            //Console.WriteLine("Enter Account Holder's Date of Birth:");
                            //newAcc.dob = Console.ReadLine();
                            //Console.WriteLine("Enter Account Holder's Aadhar Number:");
                            //newAcc.aadhar = (Console.ReadLine());
                            //Console.WriteLine("Enter Phone number:");
                            //newAcc.phno = Convert.ToDouble(Console.ReadLine());
                            //Console.WriteLine("Enter Type(Savings/Current):");
                            //newAcc.type = Console.ReadLine();
                            //Console.WriteLine("Enter Gender:");
                            //newAcc.gender = Console.ReadLine();
                            //Console.WriteLine("Enter Age :");
                            //newAcc.age = Convert.ToDouble(Console.ReadLine());
                            //Console.WriteLine("Enter  Marital Status");
                            //newAcc.maritalStatus = Console.ReadLine();
                            //Console.WriteLine("Enter Father/Husband name:");
                            //newAcc.fathOrHusbName = Console.ReadLine();
                            //Console.WriteLine("Enter Address:");
                            //Console.WriteLine("Enter town/village:");
                            //newAcc.town = Console.ReadLine();
                            //Console.WriteLine("Enter  District:");
                            //newAcc.dist = Console.ReadLine();
                            //Console.WriteLine("Enter state:");
                            //newAcc.state = Console.ReadLine();
                            //Console.WriteLine("Enter pin:");
                            //newAcc.pin = Convert.ToDouble(Console.ReadLine());
                            //Console.WriteLine("Enter Nationality:");
                            //newAcc.nationality = Console.ReadLine();
                            //Console.WriteLine("Enter Religion:");
                            //newAcc.religion = Console.ReadLine();
                            //Console.WriteLine("Enter category:");
                            //newAcc.category = Console.ReadLine();
                            //newAcc.dateOfIssue = DateTime.Now.ToString();
                            //newAcc.address = newAcc.town + "-" + newAcc.dist + "-" + newAcc.state + "-" + newAcc.pin;
                            var today = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "Created Account";
                            newAcc.trans.Add(today);
                            status = service.CreateAccountService(BankName, newAcc);
                            if (status)
                            {
                                Console.WriteLine(" Account Created and Your Account Number is {0}", newAcc.accno);
                            }
                            break;
                        }
                    case EnumServicesAvailable.DepositAmount:
                        {
                            status = false;
                            Console.WriteLine("Enter Bank Name:");
                            String BankName = Console.ReadLine();
                            try
                            {
                                new BankService().CheckBank(BankName);
                                AccNum = new Program().ReadAccountNum("");
                                Amount = new Program().ReadAmount();
                                status = service.DepositAmount(BankName, AccNum, Amount);
                            }
                            catch (BankDoesntExistException ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }
                            catch (AccountDoesntExistException ex)
                            {
                                Console.WriteLine("Accoutn doesn't exist . Please try with valid account.", ex.Message);
                            }

                            if (status)
                            {
                                Console.WriteLine("Succesfully Deposited {0}", Amount);
                            }
                            break;
                        }


                    case EnumServicesAvailable.WithDrawAmount:
                        {
                            status = false;
                            Console.WriteLine("Enter Bank Name:");
                            String BankName = Console.ReadLine();
                            try
                            {
                                new BankService().CheckBank(BankName);
                                AccNum = new Program().ReadAccountNum("");
                                Amount = new Program().ReadAmount();
                                status = service.WithdrawMoney(BankName, AccNum, Amount);
                            }
                            catch (BankDoesntExistException ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }
                            catch (AccountDoesntExistException ex)
                            {
                                Console.WriteLine("Accoutn doesn't exist . Please try with valid account.", ex.Message);
                            }
                            catch (InsufficientAmountException ex)
                            {
                                Console.WriteLine("Insufficient Amount To withdraw", ex.Message);
                            }
                            if (status)
                            {
                                Console.WriteLine("Succesfully Withdrawn {0}", Amount);
                            }
                            break;
                        }



                    case EnumServicesAvailable.TransferAmount:
                        {
                            status = false;
                            string SendAccNum = new Program().ReadAccountNum("From");
                            string RecAccNum = new Program().ReadAccountNum("To");
                            Amount = new Program().ReadAmount();
                            try
                            {
                                status = service.TransferAmount(SendAccNum, RecAccNum, Amount);
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
                                Console.WriteLine("Succesfully Transferred {0}", Amount);
                            }
                            break;
                        }


                    case EnumServicesAvailable.DisplayTransactions:
                        {
                            status = false;
                            List<String> transactions;
                            AccNum = new Program().ReadAccountNum("");
                            try
                            {
                                transactions = service.DisplayTransactions(AccNum);
                                foreach (string trans in transactions)
                                    Console.WriteLine(trans);
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
            Console.WriteLine("1.AddBank\n2.Create Account\n3.Deposit\n4.Withdrawl\n5.Transfer Money\n6.Display Transactions\nOthers:Exit");
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

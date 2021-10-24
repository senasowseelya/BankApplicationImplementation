using BankingApplication.Models;
using BankingApplication.Services;
using System;
using System.Collections.Generic;
using System.Text;
namespace BankingApplication.Consl
{
    internal class StaffActions
    {
        bool isValidStaff = false;
        bool status = false;
        String bankName;
        BankService bankService = new BankService();
        internal StaffActions()
        {
            var userCredentials = new Program().GetCredentials();
            isValidStaff = new Program().ValidateAccount(userCredentials);

            if (!isValidStaff)
            {
                Console.WriteLine("Invalid Username or Password ");
                new StaffActions();
            }
            else
            {
                while (true)
                {
                    Menu(); 
                    EnumStaffOptions Choice = (EnumStaffOptions)Enum.Parse(typeof(EnumStaffOptions), Console.ReadLine());
                    try
                    {
                        switch (Choice)
                        {
                            case EnumStaffOptions.CreateAccount:
                                {
                                    GetAndCheckBankName();
                                    CreateAccountHandler();
                                    break;
                                }
                            case EnumStaffOptions.RemoveAccount:
                                {
                                    GetAndCheckBankName();
                                    RemoveAccountHandler();
                                    break;
                                }
                                
                            case EnumStaffOptions.AddCharges:
                                {
                                    GetAndCheckBankName();
                                    AddChargesHandler();
                                    break;

                                }
                            case EnumStaffOptions.ViewTransactions:
                                {
                                    GetAndCheckBankName(); ;
                                    ViewTransactionHandler();
                                    break;
                                }
                            case EnumStaffOptions.RevertTransaction:
                                break;
                            case EnumStaffOptions.Logout:
                                break;
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
        private  void Menu()
        {
            Console.WriteLine("****XYZ Banking Service****");
            Console.WriteLine("\n1.CreateAccount\n2.RemoveAccount\n3.UpdateCharges\n4.Accept New Currency\n5. View Transaction\n6.Revert Transaction\n 7.Logout\nother.Exit");
            Console.WriteLine("***************************");
            Console.WriteLine("Enter Choice\n");
        }
        private void AddChargesHandler()
        {
            ServiceCharges serviceCharges = new ServiceCharges();
            while (true){
                Console.WriteLine("1.Self RTGS Charges\n2.Other RTGS Charges\n3.Self IMPS Charges\n4.Other IMPS Charges\n others exit");
                Console.WriteLine("Enter Choice");
                EnumServiceCharges Choice = (EnumServiceCharges)Enum.Parse(typeof(EnumServiceCharges), Console.ReadLine());
                switch (Choice)
                {
                    case EnumServiceCharges.selfRTGS:
                        Console.WriteLine("Enter  Self RTGS Charge to update");
                        serviceCharges.SelfRTGS = Convert.ToDouble(Console.ReadLine());
                        break;
                    case EnumServiceCharges.OtherRTGS:
                        Console.WriteLine("Enter Other RTGS Charge to update");
                        serviceCharges.OtherRTGS = Convert.ToDouble(Console.ReadLine());
                        break;
                    case EnumServiceCharges.selfIMPS:
                        Console.WriteLine("Enter  Self IMPS Charge to update");
                        serviceCharges.SelfIMPS = Convert.ToDouble(Console.ReadLine());
                        break;
                    case EnumServiceCharges.OtherIMPS:
                        Console.WriteLine("Enter  Other IMPS Charge to update");
                        serviceCharges.OtherIMPS = Convert.ToDouble(Console.ReadLine());
                        break;

                }
                status=bankService.AddCharges(bankName, serviceCharges);
            }
        }
        private void CreateAccountHandler()
        {
            Account newAcc = new Account();
            Console.WriteLine("Enter Account Holder's Full Name:");
            newAcc.AccholderName = Console.ReadLine();
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
            
            
            status = bankService.CreateAccountService(bankName, newAcc);
            if (status)
            {
                Console.WriteLine(" Account Created and Your Account Number is {0}", newAcc.AccountNumber);
                Console.WriteLine("Your username is {0}",newAcc.UserName);
                Console.WriteLine("Password is your accountNumber change it if you wish");
            }
            
        }
        private void RemoveAccountHandler()
        {
            Console.WriteLine("Enter account number :");
            String  accountNumber =Console.ReadLine();
            status=bankService.RemoveAccount(bankName, accountNumber);
            if (status)
            {
                Console.WriteLine("Your account is Closed ");
            }
        }
        private void ViewTransactionHandler()
        {
            List<Transaction> Transactions = bankService.ViewTransaction(bankName);
            if (Transactions != null)
            {
                foreach (var transaction in Transactions)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("No Transactions");
            }
        }
        private void GetAndCheckBankName()
        {
            Console.WriteLine("Enter Bank Name:");
             bankName = Console.ReadLine();
            bankService.CheckBank(bankName);
        }
       }
    }


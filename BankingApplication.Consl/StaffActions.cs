using BankingApplication.Models;
using BankingApplication.Services;
using System;
using System.Collections.Generic;
using System.Text;
namespace BankingApplication.Consl
{
    internal class StaffActions
    {
        string bankOfEmployee;
        bool status = false;
        String bankName;
        BankService bankService = new BankService();
        internal StaffActions()
        {
            var staffCredentials = new Program().GetCredentials();
            bankOfEmployee = new Program().ValidateStaff(staffCredentials);
            if (bankOfEmployee == null)
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
                                    if(GetAndCheckBankName())
                                        CreateAccountHandler();
                                    break;
                                }
                            case EnumStaffOptions.RemoveAccount:
                                {
                                    if (GetAndCheckBankName())
                                        RemoveAccountHandler();
                                    break;
                                }
                                
                            case EnumStaffOptions.AddCharges:
                                {
                                    if (GetAndCheckBankName())
                                        AddChargesHandler();
                                    break;

                                }
                            case EnumStaffOptions.AcceptNewCurrency:
                                {
                                    if (GetAndCheckBankName())
                                        AcceptNewCurrencyHandler();
                                    break;
                                }
                            case EnumStaffOptions.ViewTransactions:
                                {
                                    if (GetAndCheckBankName())
                                        ViewTransactionHandler();
                                    break;
                                }
                            case EnumStaffOptions.RevertTransaction:
                                {
                                    if (GetAndCheckBankName())
                                        RevertTransactionHandler();
                                    break;
                                }
                               
                            case EnumStaffOptions.Logout:
                                Program.Main();
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

        private void AcceptNewCurrencyHandler()
        {
            status = false;
            Currency newCurrency = new Currency();
            Console.WriteLine("Enter New Currency Name:");
            newCurrency.CurrencyName = Console.ReadLine();
            Console.WriteLine("Enter ExchangeRate:");
            newCurrency.ExchangeRate = Convert.ToDouble(Console.ReadLine());
            status=bankService.AcceptNewCurrency(bankName,newCurrency);
            if(status)
            {
                Console.WriteLine("Succesfully added new Currency");
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
                        {
                            Console.WriteLine("Enter  Self RTGS Charge to update");
                            serviceCharges.SelfRTGS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case EnumServiceCharges.OtherRTGS:
                        {
                            Console.WriteLine("Enter Other RTGS Charge to update");
                            serviceCharges.OtherRTGS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case EnumServiceCharges.selfIMPS:
                        {
                            Console.WriteLine("Enter  Self IMPS Charge to update");
                            serviceCharges.SelfIMPS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case EnumServiceCharges.OtherIMPS:
                        {
                            Console.WriteLine("Enter  Other IMPS Charge to update");
                            serviceCharges.OtherIMPS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    default:
                        {
                            new StaffActions();
                            break;
                        }

                }
                status=bankService.AddCharges(bankName, serviceCharges);
                if(status)
                {
                    Console.WriteLine("Updated Succesfully");
                }
            }
        }
        private void CreateAccountHandler()
        {
            Account newAcc = new Account();
            Console.WriteLine("Enter Account Holder's Full Name:");
            newAcc.AccholderName = Console.ReadLine();
            //Console.WriteLine("Enter Account Holder's Date of Birth:");
            //newAcc.DOB = Console.ReadLine();
            //Console.WriteLine("Enter Account Holder's Aadhar Number:");
            //newAcc.Aadhar = (Console.ReadLine());
            //Console.WriteLine("Enter Phone number:");
            //newAcc.PhoneNumber= Console.ReadLine();
            //Console.WriteLine("Enter Type(Savings/Current):");
            //newAcc.Type = Console.ReadLine();
            //Console.WriteLine("Enter Gender:");
            //newAcc.Gender = Console.ReadLine();
            //Console.WriteLine("Enter Age :");
            //newAcc.Age = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Enter  Marital Status");
            //newAcc.MaritalStatus = Console.ReadLine();
            //Console.WriteLine("Enter Father/Husband name:");
            //newAcc.FathOrHusbName = Console.ReadLine();
            //Console.WriteLine("Enter Address:");
            //Console.WriteLine("Enter town/village:");
            //newAcc.Town = Console.ReadLine();
            //Console.WriteLine("Enter  District:");
            //newAcc.Dist = Console.ReadLine();
            //Console.WriteLine("Enter state:");
            //newAcc.State = Console.ReadLine();
            //Console.WriteLine("Enter pin:");
            //newAcc.Pin = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Enter Nationality:");
            //newAcc.Nationality = Console.ReadLine();
            //Console.WriteLine("Enter Religion:");
            //newAcc.Religion = Console.ReadLine();
            //Console.WriteLine("Enter category:");
            //newAcc.Category = Console.ReadLine();
            //newAcc.DateofIssue = DateTime.Today;
            //newAcc.Address = newAcc.Town + "-" + newAcc.Dist + "-" + newAcc.State + "-" + newAcc.Pin;
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
                    Console.WriteLine(transaction.TransId+" "+transaction.Amount+" "+transaction.Type);
                }
            }
            else
            {
                Console.WriteLine("No Transactions");
            }
        }
        private void RevertTransactionHandler()
        {
            Console.WriteLine("Enter TransactionId to revert:");
            String TransactionId = Console.ReadLine();
            status = bankService.RevertTransaction(bankName,TransactionId);
            if (status)
            {
                Console.WriteLine("Reverted");
            }

        }
        private bool GetAndCheckBankName()
        {
            Console.WriteLine("Enter Bank Name:");
            bankName = Console.ReadLine();
            if (!bankName.Equals(bankOfEmployee))
            {
                Console.WriteLine("You are not in employees list");
                return false;
            }
            return true;
        }
       }
    }


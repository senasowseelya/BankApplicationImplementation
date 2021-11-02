using BankingApplication.Database;
using BankingApplication.Models;
using BankingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApplication.Consl
{
    internal class StaffActions
    {
        Bank bankOfEmployee;
        bool status = false;
        BankService bankService = new BankService();
        CommonFunctions commonFunctions = new CommonFunctions();
        internal StaffActions()
        {
            var staffCredentials =  commonFunctions.GetCredentials();
            bankOfEmployee = GetStaffBank(staffCredentials);
            while (bankOfEmployee == null)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Invalid Username or Password");
                Console.WriteLine("Choose\n1.Login\n2.Exit ");
                Console.WriteLine("-----------------------------------------------");
                String staffChoice = Console.ReadLine();
                if (staffChoice == "2")
                    Program.Main();
                staffCredentials = commonFunctions.GetCredentials();
                bankOfEmployee = GetStaffBank(staffCredentials);

            }
            while (true)
            {
                Menu();
                try
                {
                    switch ((EnumStaffOptions)Convert.ToInt32(Console.ReadLine()))
                    {
                        case EnumStaffOptions.AddBank:
                            {
                                Console.WriteLine("Enter Bank Name");
                                String bankName = Console.ReadLine();
                                bankService.AddBank(bankName);
                                break;
                            }
                        case EnumStaffOptions.CreateAccount:
                            {
                                CreateAccountHandler();
                                break;
                            }
                        case EnumStaffOptions.RemoveAccount:
                            {
                                RemoveAccountHandler();
                                break;
                            }
                        case EnumStaffOptions.AddCharges:
                            {
                                AddChargesHandler();
                                break;
                            }
                        case EnumStaffOptions.AcceptNewCurrency:
                            {
                                AcceptNewCurrencyHandler();
                                break;
                            }
                        case EnumStaffOptions.ViewTransactions:
                            {
                                ViewTransactionHandler();
                                break;
                            }
                        case EnumStaffOptions.RevertTransaction:
                            {
                                RevertTransactionHandler();
                                break;
                            }
                        case EnumStaffOptions.AddEmployee:
                            {
                                AddEmployeeHandler();
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

        private void AddEmployeeHandler()
        {
            status = false;
            Employee employee = new Employee();
            Console.WriteLine("Enter Name of Employee");
            employee.EmployeeName = Console.ReadLine();
            status = bankService.AddEmployee(bankOfEmployee, employee);
            commonFunctions.DisplayStatus(status, "Succesfully added new Employee\nYour username {employee.UserName} \nYour password {employee.Password}");

        }

        private void AcceptNewCurrencyHandler()
        {
            status = false;
            Currency newCurrency = new Currency();
            Console.WriteLine("Enter New Currency Name:");
            newCurrency.CurrencyName = Console.ReadLine();
            Console.WriteLine("Enter ExchangeRate:");
            newCurrency.ExchangeRate = Convert.ToDouble(Console.ReadLine());
            status = bankService.AcceptNewCurrency(bankOfEmployee, newCurrency);
            commonFunctions.DisplayStatus(status, "Succesfully added new Currency");

        }

        private void Menu()
        {
            Console.WriteLine("********XYZ Banking Service********");
            Console.WriteLine("\n1.Add Bank\n2.CreateAccount\n3.RemoveAccount\n4.UpdateCharges\n5.Accept New Currency\n6. View Transaction\n7.Revert Transaction\n 8.Add Employee\n9.Logout\nother.Exit");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Enter Choice\n");
        }
        private void AddChargesHandler()
        {
            ServiceCharges serviceCharges = new ServiceCharges();
            while (true)
            {
                Console.WriteLine("1.Self RTGS Charges\n2.Other RTGS Charges\n3.Self IMPS Charges\n4.Other IMPS Charges\n others exit");
                Console.WriteLine("Enter Choice");
                switch ((EnumServiceCharges)Convert.ToInt32(Console.ReadLine()))
                {
                    case EnumServiceCharges.SelfRTGS:
                        {
                            Console.WriteLine("Enter Self RTGS Charge to update");
                            serviceCharges.SelfRTGS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case EnumServiceCharges.OtherRTGS:
                        {
                            Console.WriteLine("Enter Other RTGS Charge to update");
                            serviceCharges.OtherRTGS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case EnumServiceCharges.SelfIMPS:
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
                            return;
                            
                        }

                }
                status = bankService.AddCharges(bankOfEmployee, serviceCharges);
                commonFunctions.DisplayStatus(status, "New Services Charges Updated Succesfully");

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
            status = bankService.CreateAccountService(bankOfEmployee, newAcc);
            if (status)
            {
                Console.WriteLine(" Account Created and Your Account Number is {0}", newAcc.AccountNumber);
                Console.WriteLine("Your username is {0}", newAcc.UserName);
                Console.WriteLine("Password is your accountNumber change it if you wish");
            }

        }
        private void RemoveAccountHandler()
        {
            Console.WriteLine("Enter account number :");
            String accountNumber = Console.ReadLine();
            status = bankService.RemoveAccount(bankOfEmployee, accountNumber);
            commonFunctions.DisplayStatus(status, "Your Account is Closed and you can't perform any actions on this acoount..");

        }
        private void ViewTransactionHandler()
        {
            List<Transaction> Transactions = bankService.ViewTransaction(bankOfEmployee);
            if (Transactions != null)
            {
                foreach (var transaction in Transactions)
                {
                    Console.WriteLine($"{transaction.TransId} {transaction.Amount} {transaction.Type}");
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
            status = bankService.RevertTransaction(bankOfEmployee, TransactionId);
            commonFunctions.DisplayStatus(status, "Transaction is Reverted successfully");

        }
        private Bank GetStaffBank(Credentials staffCredentials)
        {

            foreach (Bank bank in BankData.banks)
            {
                if (bank.Employees.Any(emp => emp.UserName.Equals(staffCredentials.UserName) && emp.Password.Equals(staffCredentials.Password)))
                    return bank;

            }
            return null;

        }

    }
}


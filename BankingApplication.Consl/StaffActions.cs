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
        
        private Bank bankOfEmployee { get; set; }
        private BankService bankService { get; set; }
        Common common =new Common();
        internal StaffActions(Credentials staffCredentials)
        {
            bankOfEmployee = GetBank(staffCredentials);
            bankService = new BankService();

        }
        internal  void StaffActivities() 
        { 
            while (true)
            {
                Menu();
                try
                {
                    switch ((StaffOptions)Convert.ToInt32(Console.ReadLine()))
                    {
                        case StaffOptions.AddBank:
                            {
                                Console.WriteLine("Enter Bank Name");
                                var bankName = Console.ReadLine();
                                bankService.AddBank(bankName);
                                break;
                            }
                        case StaffOptions.CreateAccount:
                            {
                                CreateAccount();
                                break;
                            }
                        case StaffOptions.RemoveAccount:
                            {
                                RemoveAccount();
                                break;
                            }
                        case StaffOptions.AddCharges:
                            {
                                AddCharges();
                                break;
                            }
                        case StaffOptions.AcceptNewCurrency:
                            {
                                AcceptNewCurrency();
                                break;
                            }
                        case StaffOptions.ViewTransactions:
                            {
                                ViewTransaction();
                                break;
                            }
                        case StaffOptions.RevertTransaction:
                            {
                                RevertTransaction();
                                break;
                            }
                        case StaffOptions.AddEmployee:
                            {
                                AddEmployee();
                                break;
                            }
                        case StaffOptions.Logout:
                            {
                                return;
                            }
                        case StaffOptions.Exit:
                            {
                                Environment.Exit(0);
                                break;
                            }
                            
                        default:
                            Console.WriteLine("please choose from available options");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }




        }

        private void AddEmployee()
        {
            
            var employee = new Employee();
            Console.WriteLine("Enter Name of Employee");
            employee.EmployeeName = Console.ReadLine();
            common.DisplayStatus(bankService.AddEmployee(bankOfEmployee, employee), "Succesfully added new Employee\nYour username {employee.UserName} \nYour password {employee.Password}");

        }

        private void AcceptNewCurrency()
        {
            
            var newCurrency = new Currency();
            Console.WriteLine("Enter New Currency Name:");
            newCurrency.CurrencyName = Console.ReadLine();
            Console.WriteLine("Enter ExchangeRate:");
            newCurrency.ExchangeRate = Convert.ToDouble(Console.ReadLine());
            common.DisplayStatus(bankService.AcceptNewCurrency(bankOfEmployee, newCurrency), "Succesfully added new Currency");

        }

        private void Menu()
        {
            Console.WriteLine("********XYZ Banking Service********");
            Console.WriteLine("\n1.Add Bank\n2.CreateAccount\n3.RemoveAccount\n4.UpdateCharges\n5.Accept New Currency\n6. View Transaction\n7.Revert Transaction\n 8.Add Employee\n9.Logout\n10.Exit");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Enter Choice\n");
        }
        private void AddCharges()
        {
            ServiceCharge serviceCharges = new ServiceCharge();
            while (true)
            {
                Console.WriteLine("1.Self RTGS Charges\n2.Other RTGS Charges\n3.Self IMPS Charges\n4.Other IMPS Charges\n others exit");
                Console.WriteLine("Enter Choice");
                switch ((ServiceCharges)Convert.ToInt32(Console.ReadLine()))
                {
                    case ServiceCharges.SelfRTGS:
                        {
                            Console.WriteLine("Enter Self RTGS Charge to update");
                            serviceCharges.SelfRTGS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case ServiceCharges.OtherRTGS:
                        {
                            Console.WriteLine("Enter Other RTGS Charge to update");
                            serviceCharges.OtherRTGS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case ServiceCharges.SelfIMPS:
                        {
                            Console.WriteLine("Enter  Self IMPS Charge to update");
                            serviceCharges.SelfIMPS = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case ServiceCharges.OtherIMPS:
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
                
                common.DisplayStatus(bankService.AddCharges(bankOfEmployee, serviceCharges), "New Services Charges Updated Succesfully");

            }
        }
        private void CreateAccount()
        {
            var newUser =new User();
            var newAccount = new Account();
            Console.WriteLine("Enter User's Full Name:");
            newUser.Name = Console.ReadLine();
            newAccount.AccountHolderName =newUser.Name;
            Console.WriteLine("Enter Account Holder's Date of Birth:");
            newUser.DOB = Console.ReadLine();
            Console.WriteLine("Enter Account Holder's Aadhar Number:");
            newUser.Aadhar = (Console.ReadLine());
            Console.WriteLine("Enter Phone number:");
            newUser.PhoneNumber= Console.ReadLine();
            Console.WriteLine("Enter Gender:");
            newUser.Gender = Console.ReadLine();
            Console.WriteLine("Enter Age :");
            newUser.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Father/Husband name:");
            newUser.FatherOrHusbandName = Console.ReadLine();
            Console.WriteLine("Enter Address:");
            newUser.Address = Console.ReadLine();
            Console.WriteLine("Enter Nationality:");
            newUser.Nationality = Console.ReadLine();
            Console.WriteLine("Enter Religion:");
            newUser.Religion = Console.ReadLine();
            newAccount.DateofIssue = DateTime.Today;
            if (bankService.CreateAccountService(bankOfEmployee, newAccount,newUser))
            {
                Console.WriteLine(" Account Created and Your Account Number is {0}", newAccount.AccountNumber);
                Console.WriteLine("Your username is {0}", newUser.UserName);
                Console.WriteLine("Password is your accountNumber change it if you wish");
            }

        }
        private void RemoveAccount()
        {
            Console.WriteLine("Enter account number :");
            var accountNumber = Console.ReadLine();
            
            common.DisplayStatus(bankService.RemoveAccount(bankOfEmployee, accountNumber), "Your Account is Closed and you can't perform any actions on this acoount..");

        }
        private void ViewTransaction()
        {
            var Transactions = bankService.ViewTransaction(bankOfEmployee);
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
        private void RevertTransaction()
        {
            Console.WriteLine("Enter TransactionId to revert:");
            var TransactionId = Console.ReadLine();
            
            common.DisplayStatus(bankService.RevertTransaction(bankOfEmployee, TransactionId), "Transaction is Reverted successfully");

        }
        private Bank GetBank(Credentials staffCredentials)
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


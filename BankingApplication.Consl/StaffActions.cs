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
        
        private Bank CurrentBank { get; set; }
        private BankService bankService { get; set; }
        Common common =new Common();
        internal StaffActions(Credentials staffCredentials)
        {
            CurrentBank = GetBank(staffCredentials);
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
                                AddBank();
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

        private void AddBank()
        {
            Console.WriteLine("Enter Bank Name");
            var bankName = Console.ReadLine();
            Employee employee = GetEmployeeDetails();
            bankService.AddBank(bankName,employee);
        }

        private void AddEmployee()
        {
            Employee employee = GetEmployeeDetails();
            common.DisplayStatus(bankService.AddEmployee(CurrentBank, employee), "Succesfully added new Employee\nYour username {employee.UserName} \nYour password {employee.Password}");
        }
        private Employee GetEmployeeDetails()
        {
            var employee = new Employee();
            User user= GetUserDetails();
            employee.User=user;
            employee.EmpID = $"{user.Name.Substring(0,4)}{DateTime.Today:yyyyMMDD}";
            return employee;
        }

        private void AcceptNewCurrency()
        {
            
            var newCurrency = new Currency();
            Console.WriteLine("Enter New Currency Name:");
            newCurrency.CurrencyName = Console.ReadLine();
            Console.WriteLine("Enter ExchangeRate:");
            newCurrency.ExchangeRate = Convert.ToDouble(Console.ReadLine());
            common.DisplayStatus(bankService.AcceptNewCurrency(CurrentBank, newCurrency), "Succesfully added new Currency");

        }

        private void Menu()
        {
            Console.WriteLine("********XYZ Banking Service********");
            Console.WriteLine("\n1.Add Bank\n2.CreateAccount\n3.RemoveAccount\n4.UpdateCharges\n5.Accept New Currency\n6. View Transaction\n7.Revert Transaction\n 8.Add Employee\n9.Logout\n");
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
                            CurrentBank.ServiceCharges.Find(val=>val.Name.Equals("SelfRTGS")).Value = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case ServiceCharges.OtherRTGS:
                        {
                            Console.WriteLine("Enter Other RTGS Charge to update");
                            CurrentBank.ServiceCharges.Find(val => val.Name.Equals("OtherRTGS")).Value = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case ServiceCharges.SelfIMPS:
                        {
                            Console.WriteLine("Enter  Self IMPS Charge to update");
                            CurrentBank.ServiceCharges.Find(val => val.Name.Equals("SelfIMPS")).Value = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    case ServiceCharges.OtherIMPS:
                        {
                            Console.WriteLine("Enter  Other IMPS Charge to update");
                            CurrentBank.ServiceCharges.Find(val => val.Name.Equals("SelfIMPS")).Value = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                    default:
                        {
                            return;
                            
                        }

                }
                Console.WriteLine("successfully Updated");

                new JsonReadWrite().WriteData(BankData.banks);



            }
        }
        private void CreateAccount()
        {
            User newUser=GetUserDetails();
            var newAccount = new Account();
            newAccount.AccountHolderName =newUser.Name;
            newAccount.DateofIssue = DateTime.Today;
            if (bankService.CreateAccountService(CurrentBank, newAccount,newUser))
            {
                Console.WriteLine(" Account Created and Your Account Number is {0}", newAccount.AccountNumber);
                Console.WriteLine("Your username is {0}", newUser.UserName);
                Console.WriteLine("Password is your accountNumber change it if you wish");
            }

        }
        private User GetUserDetails()
        {
            var newUser = new User();
            Console.WriteLine("Enter User's Full Name:");
            newUser.Name = Console.ReadLine();
            //Console.WriteLine("Enter  Date of Birth:");
            //newUser.DOB = Console.ReadLine();
            //Console.WriteLine("Enter  Aadhar Number:");
            //newUser.Aadhar = (Console.ReadLine());
            //Console.WriteLine("Enter Phone number:");
            //newUser.PhoneNumber = Console.ReadLine();
            //Console.WriteLine("Enter Gender:");
            //newUser.Gender = Console.ReadLine();
            //Console.WriteLine("Enter Age :");
            //newUser.Age = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Enter Father/Husband name:");
            //newUser.FatherOrHusbandName = Console.ReadLine();
            //Console.WriteLine("Enter Address:");
            //newUser.Address = Console.ReadLine();
            //Console.WriteLine("Enter Nationality:");
            //newUser.Nationality = Console.ReadLine();
            //Console.WriteLine("Enter Religion:");
            //newUser.Religion = Console.ReadLine();
            return newUser;


        }
        private void RemoveAccount()
        {
            Console.WriteLine("Enter account number :");
            var accountNumber = Console.ReadLine();
            common.DisplayStatus(bankService.RemoveAccount(CurrentBank, accountNumber), "Your Account is Closed and you can't perform any actions on this acoount..");

        }
        private void ViewTransaction()
        {
            Console.WriteLine("Enter Account Number");
            string accountNumber=Console.ReadLine();
            Account account =CurrentBank.Accounts.SingleOrDefault(acc => acc.AccountNumber.Equals(accountNumber));
            if (account == null)
                throw new AccountDoesntExistException();
            new AccountService().DisplayTransactions(account);
        }
        private void RevertTransaction()
        {
            Transaction transaction = new Transaction();
            Console.WriteLine("Enter TransactionId to revert:");
            string  transactionId = Console.ReadLine();
            Console.WriteLine("Enter Sender Account Number");
            string accountNumber = Console.ReadLine();
            foreach (Account account in CurrentBank.Accounts)
            {
                transaction = account.Transactions.SingleOrDefault(tr => tr.TransId.Equals(transactionId) && tr.Sender.Equals(accountNumber));
                if (transaction != null)
                    break;
            }
            if(transaction.TransId.Equals(""))
                Console.WriteLine("--------Invalid Transaction Id------");
            else
                common.DisplayStatus(bankService.RevertTransaction(transaction),"Transaction is Reverted successfully");
        }
        private Bank GetBank(Credentials staffCredentials)
        {

            foreach (Bank bank in BankData.banks)
            {
                if (bank.Employees.Any(emp => emp.User.UserName.Equals(staffCredentials.UserName) && emp.User.Password.Equals(staffCredentials.Password)))
                    return bank;

            }
            return null;

        }

    }
}



using BankingApplication.Data;
using BankingApplication.Services;


namespace BankingApplication.Consl
{
    internal class StaffActions
    {
        public BankDataBaseContext dbContext { get; set; }
        public string CurrentBank { get; set; }
        public Common common { get; set; }
        public BankService bankService { get; set; }
       
        
       internal StaffActions(Credentials staffCredentials) 
        {
            this.dbContext = new BankDataBaseContext();
            this.common = new Common();
            this.bankService = new BankService();
            var bankName = (from emp in dbContext.employees
                            join user in dbContext.bankusers on emp.userId equals user.id
                            where user.username.Equals(staffCredentials.UserName) && user.password.Equals(staffCredentials.Password)
                            select new
                            {
                                bankId = emp.bankId
                            }
                           ).ToList();
            CurrentBank=bankName[0].bankId;
            Console.WriteLine(CurrentBank);
        }
        internal void StaffActivities()
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
                        case StaffOptions.ModifyServiceCharges:
                            {
                                ModifyServiceCharges();
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
            Console.WriteLine("Enter Branch");
            string branch=Console.ReadLine();
            bankService.AddBank(bankName,branch);
            Console.WriteLine("Bank Added Succesfully");
        }
        private void CreateAccount()
        {
            Data.BankUser newUser = GetUserDetails();
            Console.WriteLine(" Account Created and Your Account Number is {0}", bankService.CreateAccountService(CurrentBank, newUser));
            Console.WriteLine("Your username and your password is your account number change it later");
        }
            
            

        
        private Models.BankUser GetUserDetails()
        {
            var newUser = new Models.BankUser();
            Console.WriteLine("Enter User's Full Name:");
            newUser.name = Console.ReadLine();
            Console.WriteLine("Enter  Date of Birth:");
            newUser.dob = Convert.ToDateTime(Console.ReadLine()).Date;
            Console.WriteLine("Enter  Aadhar Number:");
            newUser.aadharNumber = Convert.ToInt64((Console.ReadLine()));
            Console.WriteLine("Enter Phone number:");
            newUser.contactNumber = Console.ReadLine();
            Console.WriteLine("Enter Age :");
            newUser.age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Nationality:");
            newUser.nationality = Console.ReadLine();
            newUser.id = newUser.name.Substring(0, 4);
            newUser.username = newUser.id ;
            newUser.password = newUser.username;
            return newUser;
        }
        private void RemoveAccount()
        {
            Console.WriteLine("Enter account number :");
            var accountNumber = Console.ReadLine();
            common.DisplayStatus(bankService.RemoveAccount(CurrentBank, accountNumber), "Your Account is Closed and you can't perform any actions on this acoount..");
            
            

        }
        private void ModifyServiceCharges()
        {
            Console.WriteLine("Enter ServiceCharge id:");
            var serviceChargeId=Console.ReadLine();
            Console.WriteLine("Enter Modified Value");
            var serviceChargeValue=Convert.ToDecimal(Console.ReadLine());
            common.DisplayStatus(bankService.ModifyServiceCharges(CurrentBank,serviceChargeId, serviceChargeValue),"Changed value Successfully");


        }
        private void AcceptNewCurrency()
        {

            var newCurrency = new Models.Currency();
            Console.WriteLine("Enter currency Id");
            newCurrency.id = Console.ReadLine();
            Console.WriteLine("Enter New Currency Name:");
            newCurrency.name = Console.ReadLine();
            Console.WriteLine("Enter ExchangeRate:");
            newCurrency.exchangeRate = Convert.ToDecimal(Console.ReadLine());
            common.DisplayStatus(bankService.AcceptNewCurrency(CurrentBank, newCurrency), "Succesfully added new Currency");

        }
        private void ViewTransaction()
        {
            Console.WriteLine("Enter Account Id");
            string accountNumber = Console.ReadLine();
            List<Data.Transaction> transactions = new List<Data.Transaction>();
            transactions = bankService.DisplayTransactions(accountNumber);
            foreach(Data.Transaction  transaction in transactions)
            {
                Console.WriteLine(transaction.transid+" "+transaction.transactionAmount+" "+transaction.transactionOn+" "+transaction.senderaccountId+" "+transaction.receiveraccountId);
            }

        }


        private void AddEmployee()
        {
            Models.BankUser user = GetUserDetails();
            bankService.AddEmployee(CurrentBank, user);
            common.DisplayStatus(true, $"Succesfully added new Employee\nYour username {user.username} \nYour password {user.password}");
        }
        



        private void Menu()
        {
            Console.WriteLine("********XYZ Banking Service********");
            Console.WriteLine("\n1.Add Bank\n2.CreateAccount\n3.RemoveAccount\n4.UpdateCharges\n5.Accept New Currency\n6. View Transaction\n7.Revert Transaction\n 8.Add Employee\n9.Logout\n");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Enter Choice\n");
        }



        private void RevertTransaction()
        {
            Transaction transaction = new Transaction();
            Console.WriteLine("Enter TransactionId to revert:");
            string transactionId = Console.ReadLine();
            Console.WriteLine("Enter Sender Account Id");
            string accountId = Console.ReadLine();
            common.DisplayStatus(bankService.RevertTransaction(transactionId,accountId), "Transaction is Reverted successfully");
        }
        

    }
}


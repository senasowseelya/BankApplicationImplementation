using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingApplication.Database;
using BankingApplication.Models;


namespace BankingApplication.Services
{
    public class BankService
    {
        JsonReadWrite dataReadWrite = new JsonReadWrite();
        public bool AddBank(String name)
        {
            if (BankData.banks.Any(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new BankAlreadyExistsException();
            Bank newBank = new Bank(name);
            newBank.Employees.Add(new Employee());
            BankData.banks.Add(newBank);
            dataReadWrite.WriteData(BankData.banks);
            return true;
        }
        public bool AddEmployee(Bank bank, Employee newEmployee)
        {
            newEmployee.EmpID = $"{newEmployee.BankId}{newEmployee.EmployeeName}";
            newEmployee.UserName = $"{newEmployee.EmployeeName.Substring(0, 3)}{newEmployee.EmpID.Substring(4, 3)}";
            newEmployee.Password = $"{newEmployee.EmpID}";
            bank.Employees.Add(newEmployee);
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        public bool CreateAccountService(Bank bank, Account newAcc, User newUser)
        {
            newAcc.AccountNumber = GenerateAccountNumber();
            newAcc.AccountId = GenerateAccId(bank.Name);
            newAcc.BankID = bank.BankId;
            newUser.UserName = newAcc.AccountHolderName.Substring(0, 4);
            newUser.Password = newAcc.AccountNumber;
            newAcc.IsActive = true;
            newAcc.User = newUser;
            bank.Accounts.Add(newAcc);
            dataReadWrite.WriteData(BankData.banks);
            return true;
        }

        public bool RemoveAccount(Bank bank, String accountNumber)
        {

            Account account = bank.Accounts.SingleOrDefault(acc => acc.AccountNumber.Equals(accountNumber));
            if (account != null)
            {
                account.IsActive = false;
                dataReadWrite.WriteData(BankData.banks);
                return true;
            }
            throw new AccountDoesntExistException();

        }
        public bool AddCharges(Bank bank, ServiceCharge serviceCharges)
        {
            bank.ServiceCharges = serviceCharges;
            dataReadWrite.WriteData(BankData.banks);
            return true;
        }
        public List<Transaction> ViewTransaction(Bank bank)
        {
            return bank.BankTransactions;
        }
        public bool RevertTransaction(Bank bank, string transactionId)
        {
            Transaction transaction = bank.BankTransactions.FirstOrDefault(tr => tr.TransId.Equals(transactionId));
            transaction.Type = TransactionType.Revert;
            Account senderAccount = FetchAccount(transaction.Sender);
            Account receiverAccount = FetchAccount(transaction.Receiver);
            Transaction sendtrans = senderAccount.Transactions.FirstOrDefault(tr => tr.TransId.Equals(transactionId));
            Transaction rectrans = receiverAccount.Transactions.FirstOrDefault(tr => tr.TransId.Equals(transactionId));
            if (senderAccount.Equals(receiverAccount))
            {
                if (transaction.Type.Equals(TransactionType.Credited))
                    senderAccount.Balance -= transaction.Amount;
                else if (transaction.Type.Equals(TransactionType.Debited))
                    senderAccount.Balance += transaction.Amount;
                sendtrans.Type = TransactionType.Revert;
                dataReadWrite.WriteData(BankData.banks);
                return true;
            }
            else
            {
                senderAccount.Balance += transaction.Amount;
                sendtrans.Type = TransactionType.Revert;
                receiverAccount.Balance -= transaction.Amount;
                sendtrans.Type = TransactionType.Revert;
                dataReadWrite.WriteData(BankData.banks);
                return true;
            }
            throw new InvalidTransactionException();


        }
        public bool AcceptNewCurrency(Bank bank, Currency newCurrency)
        {
            Currency currency = bank.AcceptedCurrencies.SingleOrDefault(acc => acc.CurrencyName.Equals(newCurrency.CurrencyName));
            if (currency != null)
                throw new DuplicateCurrencyException();
            bank.AcceptedCurrencies.Add(newCurrency);
            dataReadWrite.WriteData(BankData.banks);
            return true;
        }

        public Account FetchAccount(String accountNumber)
        {
            foreach (Bank bank in BankData.banks)
            {
                Account account = bank.Accounts.SingleOrDefault(acc => acc.AccountNumber.Equals(accountNumber) && acc.IsActive);
                if (account != null)
                    return account;
            }
            throw new AccountDoesntExistException();
        }
        private String GenerateAccountNumber()
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;

        }
        private String GenerateAccId(String BankName)
        {
            string AccID = BankName.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd");
            return AccID;
        }

    }
}

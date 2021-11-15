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
        JsonReadWrite dataReadWrite;
        public BankService()
        {
            dataReadWrite = new JsonReadWrite();
        }
        public bool AddBank(string name,Employee employee)
        {
            if (BankData.banks.Any(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new BankAlreadyExistsException();
            var newBank = new Bank(name);
            AddEmployee(newBank, employee);
            BankData.banks.Add(newBank);
            dataReadWrite.WriteData(BankData.banks);
            return true;
        }
        public bool AddEmployee(Bank bank, Employee newEmployee)
        {
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
            newUser.UserId = newAcc.AccountId.Substring(0, 4)+newUser.UserName;
            newAcc.IsActive = true;
            newAcc.User = newUser;
            bank.Accounts.Add(newAcc);
            dataReadWrite.WriteData(BankData.banks);
            return true;
        }

        public bool RemoveAccount(Bank bank, String accountNumber)
        {

            var account = bank.Accounts.SingleOrDefault(acc => acc.AccountNumber.Equals(accountNumber));
            if (account != null)
            {
                account.IsActive = false;
                dataReadWrite.WriteData(BankData.banks);
                return true;
            }
            throw new AccountDoesntExistException();

        }
       
        
        public bool RevertTransaction(Transaction transaction)
        {
            if(transaction.Sender==null || transaction.Receiver==null)
            {
                return false;
            }
            var senderAccount = FetchAccount(transaction.Sender);
            var receiverAccount = FetchAccount(transaction.Receiver);
            var senderTransaction = senderAccount.Transactions.FirstOrDefault(tr => tr.TransId.Equals(transaction.TransId));
            var receiverTransaction = receiverAccount?.Transactions.FirstOrDefault(tr => tr.TransId.Equals(transaction.TransId));
            senderAccount.Balance += transaction.Amount;
            receiverAccount.Balance-= transaction.Amount;
            senderTransaction.Type = TransactionType.Revert;
            receiverTransaction.Type= TransactionType.Revert;
            dataReadWrite.WriteData(BankData.banks);
            return true;

        }
        public bool AcceptNewCurrency(Bank bank, Currency newCurrency)
        {
            var currency = bank.AcceptedCurrencies.SingleOrDefault(acc => acc.CurrencyName.Equals(newCurrency.CurrencyName));
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
                var account = bank.Accounts.SingleOrDefault(acc => acc.AccountNumber.Equals(accountNumber) && acc.IsActive);
                if (account != null)
                    return account;
            }
            throw new AccountDoesntExistException();
        }
        private String GenerateAccountNumber()
        {
            var random = new Random();
            var r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;

        }
        private String GenerateAccId(String BankName)
        {
            var AccID = BankName.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd");
            return AccID;
        }

    }
}

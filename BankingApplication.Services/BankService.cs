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
        
        public bool AddBank(String name)
        {
            if (BankData.banks.Any(e => e.Name == name))
                throw new BankAlreadyExistsException();
            Bank newBank = new Bank();
            newBank.BankId = name.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd"); ;
            newBank.Name = name;
            newBank.IFSC = newBank.BankId + GenerateAccountNumber();
            Currency newCurrency = new Currency();
            newCurrency.CurrencyName = "INR";
            newCurrency.ExchangeRate = 1.0;
            newBank.AcceptedCurrencies.Add(newCurrency);
            newBank.DefaultCurrency=newCurrency;
            newBank.SelfIMPS = 5.0;
            newBank.SelfRTGS = 0.0;
            newBank.OtherRTGS = 2.0;
            newBank.OtherIMPS = 6.0;
            BankData.banks.Add(newBank);
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        public bool CreateAccountService(Bank bank, Account newAcc)
        {
                    newAcc.AccountNumber = GenerateAccountNumber();
                    newAcc.AccountId = GenerateAccId(bank.Name);
                    newAcc.BankID = bank.BankId;
                    newAcc.UserName = newAcc.AccholderName.Substring(0, 4);
                    newAcc.Password = newAcc.AccountNumber;
                    newAcc.IsActive = true;
                    bank.Accounts.Add(newAcc);
                    new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        public bool RemoveAccount(Bank bank, String accountNumber)
        {

            Account account = FetchAccount(accountNumber);
            account.IsActive = false;
            bank.Accounts.Remove(account);
            bank.InActiveAccounts.Add(account);
            new JsonReadWrite().WriteData(BankData.banks);
            return true;

        }
        public bool AddCharges(Bank bank, ServiceCharges serviceCharges)
        {
            
            bank.SelfIMPS = serviceCharges.SelfIMPS;
            bank.SelfRTGS = serviceCharges.SelfRTGS;
            bank.OtherIMPS = serviceCharges.OtherIMPS;
            bank.OtherRTGS = serviceCharges.OtherRTGS;
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        public List<Transaction> ViewTransaction(Bank bank)
        {
            return bank.BankTransactions;
        }
        public bool RevertTransaction(Bank bank, string transactionId)
        {
            Transaction transaction = bank.BankTransactions.FirstOrDefault(tr => tr.TransId.Equals(transactionId));
            bank.BankTransactions.Remove(transaction);
            Account senderAccount =FetchAccount(transaction.Sender);
            Account receiverAccount = FetchAccount(transaction.Receiver);
            Transaction sendtrans= senderAccount.Transactions.FirstOrDefault(tr => tr.TransId.Equals(transactionId));
            Transaction rectrans = receiverAccount.Transactions.FirstOrDefault(tr => tr.TransId.Equals(transactionId));
            if (senderAccount.Equals(receiverAccount))
            {
                if (transaction.Type.Equals(EnumTypeofTransactions.Credited))
                    senderAccount.Balance -= transaction.Amount;
                else if (transaction.Type.Equals(EnumTypeofTransactions.Debited))
                    senderAccount.Balance += transaction.Amount;
                senderAccount.Transactions.Remove(sendtrans);
                new JsonReadWrite().WriteData(BankData.banks);
                return true;
            }
            else
            {
                senderAccount.Balance += transaction.Amount;
                senderAccount.Transactions.Remove(sendtrans);
                receiverAccount.Balance -= transaction.Amount;
                receiverAccount.Transactions.Remove(rectrans);
                new JsonReadWrite().WriteData(BankData.banks);
                return true;
            }
            throw new InvalidTransactionException();


        }
        public bool AcceptNewCurrency(Bank bank, Currency newCurrency)
        {
            bank.AcceptedCurrencies.Add(newCurrency);
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        
        public Account FetchAccount(String accountNumber)
        {
            foreach (Bank bank in BankData.banks)
            {
                foreach (Account account in bank.Accounts)
                {
                    if (account.AccountNumber == accountNumber)
                    {
                        return account;
                    }
                }
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

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
        List<Bank> banks = new JsonReadWrite().ReadData();

        public bool AddBank(String name)
        {

            if (banks.Any(e => e.Name == name))
            {
                throw new BankAlreadyExistsException();
            }
            Bank newBank = new Bank();
            newBank.BankId = name.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd"); ;
            newBank.Name = name;
            newBank.IFSC = newBank.BankId + GenerateAccountNumber();
            banks.Add(newBank);
            new JsonReadWrite().WriteData(banks);
            return true;
        }
        public bool CreateAccountService(string bankName, Account newAcc)
        {
            Bank bank = FetchBankByName(bankName);
            newAcc.AccountNumber = GenerateAccountNumber();
            newAcc.AccountId = GenerateAccId(bankName);
            newAcc.BankID = bank.BankId;
            newAcc.UserName = newAcc.AccholderName.Substring(0, 4);
            newAcc.Password = newAcc.AccountNumber;
            newAcc.IsActive = true;
            bank.Accounts.Add(newAcc);
            new JsonReadWrite().WriteData(banks);
            return true;
        }
        public bool RemoveAccount(String bankName, String accountNumber)
        {

            Account account = new AccountService().FetchAccount(accountNumber);
            Bank bank = FetchBankByName(bankName);
            account.IsActive = false;
            bank.InActiveAccounts.Add(account);
            bank.Accounts.Remove(account);
            new JsonReadWrite().WriteData(banks);
            return true;

        }
        public bool CheckBank(String BankName)
        {
            if (!banks.Any(e => e.Name == BankName))
                throw new BankDoesntExistException();
            return true;
        }
        public bool AddCharges(String bankName, ServiceCharges serviceCharges)
        {
            Bank bank = FetchBankByName(bankName);
            bank.SelfIMPS = serviceCharges.SelfIMPS;
            bank.SelfRTGS = serviceCharges.SelfRTGS;
            bank.OtherIMPS = serviceCharges.OtherIMPS;
            bank.OtherRTGS = serviceCharges.OtherRTGS;
            new JsonReadWrite().WriteData(banks);
            return true;
        }
        public List<Transaction> ViewTransaction(String bankName)
        {
            Bank bank = FetchBankByName(bankName);
            return bank.BankTransactions;



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
        public bool RevertTransaction(string bankName, string transactionId)
        {
            Bank bank = FetchBankByName(bankName);
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
                new JsonReadWrite().WriteData(banks);
                return true;
            }
            else
            {
                senderAccount.Balance += transaction.Amount;
                senderAccount.Transactions.Remove(sendtrans);
                receiverAccount.Balance -= transaction.Amount;
                receiverAccount.Transactions.Remove(rectrans);
                new JsonReadWrite().WriteData(banks);
                return true;
            }
            throw new InvalidTransactionException();


        }
        public bool AcceptNewCurrency(String bankName, Currency newCurrency)
        {
            Bank bank = FetchBankByName(bankName);
            bank.AcceptedCurrencies.Add(newCurrency);
            new JsonReadWrite().WriteData(banks);
            return true;
        }
        internal Bank FetchBankByName(String bankName)
        {
            foreach (Bank bank in banks)
            {
                if (bank.Name == bankName)
                {
                    return bank;

                }
            }
            throw new BankDoesntExistException();
        }
        public Account FetchAccount(String accountNumber)
        {
            foreach (Bank bank in banks)
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

    }
}

using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BankingApplication.Services
{
    public class AccountService
    {
        BankDataBaseContext dbContext = new BankDataBaseContext();
        Currency? currencyObj;
        Account? account;
        Transaction? transaction;
        public bool Deposit(string accNumber, decimal amount, string currencyName)
        {
            account = FetchAccount(accNumber);
            currencyObj = FetchCurrency(account.bankId, currencyName);
            account.balance += (amount * currencyObj.exchangeRate);
            transaction = GenerateTransaction(account, account, currencyObj, amount, "1");
            dbContext.transactions.Add(transaction);
            dbContext.SaveChanges();
            return true;

        }

        private Transaction? GenerateTransaction(Account sendAccount, Account recAccount, Currency currency, decimal amount, string type)
        {
            Transaction transaction = new Transaction();
            transaction.receiveraccountId = recAccount.accountId;
            transaction.senderaccountId = sendAccount.accountId;
            transaction.transid = $"TXN {sendAccount.bankId} {DateTime.Now.ToString("yyyyMMddhhmmss")}";
            transaction.transactionAmount = amount;
            transaction.type = type;
            transaction.transactionOn = DateTime.Today;
            transaction.currency = currency.name;
            transaction.currency1 = currency;
            return transaction;







        }

        public bool Withdraw(string accNumber, decimal amount, string currencyName)
        {
            account = FetchAccount(accNumber);
            currencyObj = FetchCurrency(account.bankId, currencyName);
            if (account.balance >= amount * currencyObj.exchangeRate)
            {
                account.balance -= amount * currencyObj.exchangeRate;
                transaction = GenerateTransaction(account, account, currencyObj, amount, "2");
                dbContext.transactions.Add(transaction);
                dbContext.SaveChanges();
                return true;
            }
            throw new InsufficientAmountException();
        }
        private Account FetchAccount(string accNumber)
        {
            account = (from acc in dbContext.accounts where acc.accountNumber == accNumber && acc.status=="Active" select acc).FirstOrDefault();
            if (account != null)
                return account;
            else
                throw new AccountDoesntExistException();
        }
        private Currency FetchCurrency(string bankId, string currencyName)
        {
            currencyObj = (from currency in dbContext.currencies where currency.bankid == bankId && currency.name == currencyName.ToUpper() select currency).FirstOrDefault();
            if (currencyObj == null)
                throw new CurrencyNotSupportedException();
            else
                return currencyObj;
        }
        public List<Transaction> Displaytransactions(string accNumber)
        {
            account = FetchAccount(accNumber);
            List<Transaction> transactions = (from transaction in dbContext.transactions where transaction.receiveraccountId == account.accountId || transaction.senderaccountId == account.accountId select transaction).ToList();
            return transactions;
        }
        public bool ChangePassword(string accNumber, string oldpassword, string newPassword)
        {
            account = FetchAccount(accNumber);
            BankUser bankuser = (from user in dbContext.bankusers where user.id == account.userId && user.password == oldpassword select user).SingleOrDefault();
            if (bankuser == null)
                return false;
            else
            {
                bankuser.password = newPassword;
                dbContext.SaveChanges();
            }
            return true;
        }
        public bool TransferAmount(string sendAccNumber, string recAccNumber, decimal amount, string currencyName, string mode)
        {
            decimal charges = 0;
            Account senderAccount = FetchAccount(sendAccNumber);
            Account receiverAccount = FetchAccount(recAccNumber);
            Bank senderBank = (from bank in dbContext.banks where bank.id == senderAccount.bankId select bank).SingleOrDefault();
            currencyObj = FetchCurrency(senderAccount.bankId, currencyName);
            if (senderAccount.bankId == receiverAccount.bankId)
            {
                if (mode == "1")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "SelfRTGS" select charge.value).SingleOrDefault();
                if (mode == "2")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "SelfIMPS" select charge.value).SingleOrDefault();

            }
            else
            {
                if (mode == "1")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "OtherRTGS" select charge.value).SingleOrDefault();
                if (mode == "2")
                    charges = (from charge in dbContext.serviceCharges where charge.bankId == senderAccount.bankId && charge.name == "OtherIMPS" select charge.value).SingleOrDefault();


            }
            if (senderAccount.balance >= amount)
            {
                senderAccount.balance -= amount;
                receiverAccount.balance += amount;
                senderBank.balance -= charges;
                transaction = GenerateTransaction(senderAccount, receiverAccount, currencyObj, amount, "3");
                dbContext.transactions.Add(transaction);
                dbContext.SaveChanges();
                return true;

            }
            else
            {
                throw new InsufficientAmountException();
            }
            
        }


    }
}

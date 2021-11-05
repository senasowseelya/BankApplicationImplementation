using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Models;
using BankingApplication.Database;
using System.Reflection;
using System.Linq;

namespace BankingApplication.Services
{
    public class AccountService
    
    {

        Currency currency;
        public AccountService()
        {
            currency = new Currency();  
        }
        public bool Deposit( Account userAccount ,double amount,String currencyName)
        {
            var bank = FetchBank(userAccount.BankID);
            amount = amount * GetCurrencyExchangeRate(bank, currencyName) ;
            userAccount.Balance += amount;
            new JsonReadWrite().WriteData(BankData.banks);
            Transaction transaction =new Transaction();
            transaction.Sender = null;
            transaction.Receiver = userAccount.AccountNumber;
            transaction.Type = TransactionType.Credited;
            transaction.Amount = amount;    
            GenerateTransaction(transaction,currencyName);
            return true;
        }

        private Double GetCurrencyExchangeRate(Bank bank,String name)
        {
            var currency = bank.AcceptedCurrencies.SingleOrDefault(cr=>cr.CurrencyName.Equals(name,StringComparison.OrdinalIgnoreCase));
            if (currency != null)
                return currency.ExchangeRate;
            throw new CurrencyNotSupportedException();
            
        }

        public bool Withdraw(Account userAccount, double amount,string currencyName)
        {
            var bank = FetchBank(userAccount.BankID);
            amount = amount * GetCurrencyExchangeRate(bank, currencyName);
            Transaction transaction = new Transaction();
            transaction.Sender = userAccount.AccountNumber;
            transaction.Receiver = null;
            transaction.Amount = amount;
            if (userAccount.Balance >= amount)
            {
                userAccount.Balance -= amount;
                new JsonReadWrite().WriteData(BankData.banks);
                transaction.Type = TransactionType.Debited;
                GenerateTransaction(transaction, currencyName);
                
                return true;
            }
            else
            {
               transaction.Type = TransactionType.Failed;
               GenerateTransaction(transaction, currencyName);
               throw new InsufficientAmountException();
            }
        }
        public bool TransferAmount(Account senderAccount, string toAccNum, double amount,ModeOfTransfer mode)
        {
            var senderBank = FetchBank(senderAccount.BankID);
            var receiverAccount =new BankService().FetchAccount(toAccNum);
            var receiverBank = FetchBank(receiverAccount.BankID);
            amount = amount * senderBank.DefaultCurrency.ExchangeRate;
            var charge = CalculateCharges(senderBank,receiverBank,amount,mode);
            if (senderAccount.Balance >= amount+charge)
            {
                receiverAccount.Balance += amount;
                senderAccount.Balance -= (amount+charge);
                senderBank.BankBalance += charge;
                new JsonReadWrite().WriteData(BankData.banks);
                Transaction transaction = new Transaction();
                transaction.Sender = senderAccount.AccountNumber;
                transaction.Receiver = toAccNum;
                transaction.Type = TransactionType.Transfer;
                transaction.Amount = amount;
                GenerateTransaction(transaction, senderBank.DefaultCurrency.CurrencyName);
                transaction.Type = TransactionType.ServiceCharges;
                GenerateTransaction(transaction, senderBank.DefaultCurrency.CurrencyName);
            }
            else
                throw new InsufficientAmountException();
            return true;
        }
        public List<Transaction> DisplayTransactions(Account userAccount)
        {
            return userAccount.Transactions;
        }
        public bool ChangePassword(Account account ,String newPassword)
        {
            account.User.Password = newPassword;
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        private void GenerateTransaction(Transaction transaction,String currencyName)
        {
            var bankService = new BankService();
            string bankId = "";
            var senderAccount = new Account();
            var receiverAccount = new Account();
            if (transaction.Sender != null)
            {
                 senderAccount = bankService.FetchAccount(transaction.Sender);
                 bankId = senderAccount.BankID;
            }
            if (transaction.Receiver != null)
            {
                 receiverAccount = bankService.FetchAccount(transaction.Receiver);
                if(bankId=="")
                    bankId=receiverAccount.BankID;
            }
            var NewTransaction = new Transaction(bankId);
            NewTransaction.Sender = transaction.Sender;
            NewTransaction.Receiver = transaction.Receiver;
            NewTransaction.Type = transaction.Type;
            NewTransaction.CurrencyName = currencyName;
            NewTransaction.Amount = transaction.Amount;
            senderAccount.Transactions.Add(NewTransaction);
            if(receiverAccount!=null)
            {
                receiverAccount.Transactions.Add(NewTransaction);
            }
            new JsonReadWrite().WriteData(BankData.banks);

        }
       
        public Bank FetchBank(String bankId)
        {
            var bank = BankData.banks.FirstOrDefault(bk => bk.BankId == bankId);
            if (bank != null)
                return bank;
            throw new BankDoesntExistException();
        }
        
        private Double CalculateCharges(Bank senderBank,Bank receiverBank,Double amount,ModeOfTransfer mode)
        {
            Double charge = 0.0;
            switch (mode)
            {
                case ModeOfTransfer.IMPS:
                    {
                        if (senderBank.BankId.Equals(receiverBank.BankId))
                        {
                            charge = amount * (senderBank.ServiceCharges.SelfIMPS / 100);
                        }
                        else
                        {
                            charge = amount * (senderBank.ServiceCharges.OtherIMPS / 100);
                        }
                        break;
                    }
                case ModeOfTransfer.RTGS:
                    {
                        if (senderBank.BankId.Equals(receiverBank.BankId))
                        {
                            charge = amount * (senderBank.ServiceCharges.SelfRTGS / 100);
                        }
                        else
                        {
                            charge = amount * (senderBank.ServiceCharges.OtherRTGS / 100);
                        }
                        break;
                    }

            }
            return charge;
        }
    }

}


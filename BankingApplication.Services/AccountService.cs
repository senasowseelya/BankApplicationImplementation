using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Models;
using BankingApplication.Database;

namespace BankingApplication.Services
{
    public class AccountService
    
    {
        
        Currency currency = new Currency();

        public bool Deposit( Account userAccount ,double amount,String currencyName)
        {
            Bank bank = FetchBank(userAccount.BankID);
            currency = GetCurrencyobject(bank,currencyName);
            amount = amount * currency.ExchangeRate;
            userAccount.Balance += amount;
            Transaction NewTransaction = GenerateTransaction(userAccount,userAccount,amount, EnumTypeofTransactions.Credited,currencyName);
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }

        private Currency GetCurrencyobject(Bank bank,String name)
        {
            foreach(Currency currency in bank.AcceptedCurrencies)
            {
                if(currency.CurrencyName==name)
                {
                    return currency;
                }
            }
            throw new CurrencyNotSupportedException();
            
        }

        public bool Withdraw(Account userAccount, double amount,string currencyName)
        {
            Bank bank = FetchBank(userAccount.BankID);
            currency = GetCurrencyobject(bank, currencyName);
            amount = amount * currency.ExchangeRate;
            if (userAccount.Balance >= amount)
            {
                userAccount.Balance -= amount;
                Transaction NewTransaction = GenerateTransaction(userAccount,userAccount, amount, EnumTypeofTransactions.Debited,currencyName);
                new JsonReadWrite().WriteData(BankData.banks);
                return true;
            }
            else
                throw new InsufficientAmountException();
        }
        public bool TransferAmount(Account senderAccount, string toAccNum, double amount,EnumModeOfTransfer mode,string currencyName )
        {
            Bank senderBank = FetchBank(senderAccount.BankID);
            Account receiverAccount = FetchAccount(toAccNum);
            Bank receiverBank = FetchBank(receiverAccount.BankID);
            currency = GetCurrencyobject(senderBank, currencyName);
            amount = amount * currency.ExchangeRate;
            Double charge = CalculateCharges(senderBank,receiverBank,amount,mode);
            if (senderAccount.Balance >= amount+charge)
            {
                receiverAccount.Balance += amount;
                senderAccount.Balance -= (amount+charge);
                GenerateTransaction(senderAccount,receiverAccount, amount, EnumTypeofTransactions.Transfer,currencyName);
                senderBank.BankBalance +=charge;
                new JsonReadWrite().WriteData(BankData.banks);
            }
            else
                throw new InsufficientAmountException();
            return true;
        }
        public List<Transaction> DisplayTransactions(Account userAccount)
        {
            return userAccount.Transactions;
        }
        private Transaction GenerateTransaction(Account senderAccount,Account recAccount, Double Amount,EnumTypeofTransactions type,String currencyName)
        {

            Transaction NewTransaction = new Transaction();
            Bank bank = FetchBank(senderAccount.BankID);
            NewTransaction.TransId = $"TXN{bank.BankId.Substring(0, 3)}{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}";
            NewTransaction.Sender = senderAccount.AccountNumber;
            NewTransaction.Receiver =recAccount.AccountNumber;
            NewTransaction.Type = type;
            NewTransaction.CurrencyName = currencyName;
            NewTransaction.Amount = Amount;
            senderAccount.Transactions.Add(NewTransaction);
            if (senderAccount != recAccount)
            {
                recAccount.Transactions.Add(NewTransaction);
            }
            bank.BankTransactions.Add(NewTransaction);
            return NewTransaction;

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
        public Bank FetchBank(String bankId)
        {
            foreach (Bank bank in BankData.banks)
            {
                if (bank.BankId == bankId)
                {
                    return bank;

                }
            }
            throw new BankDoesntExistException();
        }
        
        private Double CalculateCharges(Bank senderBank,Bank receiverBank,Double amount,EnumModeOfTransfer mode)
        {
            Double charge = 0.0;
            switch (mode)
            {
                case EnumModeOfTransfer.IMPS:
                    {
                        if (senderBank == receiverBank)
                        {
                            charge = amount * (senderBank.SelfIMPS / 100);
                        }
                        else
                        {
                            charge = amount * (senderBank.OtherIMPS / 100);
                        }
                        break;
                    }
                case EnumModeOfTransfer.RTGS:
                    {
                        if (senderBank == receiverBank)
                        {
                            charge = amount * (senderBank.SelfRTGS / 100);
                        }
                        else
                        {
                            charge = amount * (senderBank.OtherRTGS / 100);
                        }
                        break;
                    }

            }
            return charge;
        }
    }

}


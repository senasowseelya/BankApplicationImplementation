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
         
        Currency currency = new Currency();
        public bool Deposit( Account userAccount ,double amount,String currencyName)
        {
            Bank bank = FetchBank(userAccount.BankID);
            currency = GetCurrencyobject(bank,currencyName);
            amount = amount * currency.ExchangeRate;
            userAccount.Balance += amount;
            GenerateTransaction(userAccount,userAccount,amount, TransactionType.Credited,currencyName);
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }

        private Currency GetCurrencyobject(Bank bank,String name)
        {
            Currency currency = bank.AcceptedCurrencies.SingleOrDefault(cr=>cr.CurrencyName.Equals(name,StringComparison.OrdinalIgnoreCase));
            if (currency != null)
                return currency;
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
                GenerateTransaction(userAccount,userAccount, amount, TransactionType.Debited,currencyName);
                new JsonReadWrite().WriteData(BankData.banks);
                return true;
            }
            else
                throw new InsufficientAmountException();
        }
        public bool TransferAmount(Account senderAccount, string toAccNum, double amount,ModeOfTransfer mode)
        {
            Bank senderBank = FetchBank(senderAccount.BankID);
            Account receiverAccount =new BankService().FetchAccount(toAccNum);
            Bank receiverBank = FetchBank(receiverAccount.BankID);
            amount = amount * senderBank.DefaultCurrency.ExchangeRate;
            Double charge = CalculateCharges(senderBank,receiverBank,amount,mode);
            if (senderAccount.Balance >= amount+charge)
            {
                receiverAccount.Balance += amount;
                senderAccount.Balance -= (amount+charge);
                GenerateTransaction(senderAccount,receiverAccount, amount, TransactionType.Transfer, senderBank.DefaultCurrency.CurrencyName);
                GenerateTransaction(senderAccount, receiverAccount, charge, TransactionType.ServiceCharges, senderBank.DefaultCurrency.CurrencyName);
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
        public bool ChangePassword(Account account ,String newPassword)
        {
            account.User.Password = newPassword;
            new JsonReadWrite().WriteData(BankData.banks);
            return true;
        }
        private void GenerateTransaction(Account senderAccount,Account recAccount, Double Amount,TransactionType type,String currencyName)
        {
            Bank bank = FetchBank(senderAccount.BankID);
            Transaction NewTransaction = new Transaction(bank);
            NewTransaction.Sender = senderAccount.AccountNumber;
            NewTransaction.Receiver =recAccount.AccountNumber;
            NewTransaction.Type = type;
            NewTransaction.CurrencyName = currencyName;
            NewTransaction.Amount = Amount;
            senderAccount.Transactions.Add(NewTransaction);
            if(!senderAccount.AccountNumber.Equals(recAccount.AccountNumber))
            {
                recAccount.Transactions.Add(NewTransaction);
            }
            bank.BankTransactions.Add(NewTransaction);
        }
       
        public Bank FetchBank(String bankId)
        {
            Bank bank = BankData.banks.FirstOrDefault(bk => bk.BankId == bankId);
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


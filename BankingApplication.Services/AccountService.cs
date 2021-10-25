using System;
using System.Collections.Generic;
using System.Text;
using BankingApplication.Models;
using BankingApplication.Database;
namespace BankingApplication.Services
{
    public class AccountService
    
    {
        List<Bank> banks = new JsonReadWrite().ReadData();
        Currency currency = new Currency();
        public bool Deposit(string accNum, double amount,String currencyName)
        {
            Account account = FetchAccount(accNum);
            Console.WriteLine(amount);
            Bank bank = FetchBank(account.BankID);
            currency = GetCurrencyobject(bank,currencyName);
            amount = amount * currency.ExchangeRate;
            account.Balance += amount;
            Transaction NewTransaction = GenerateTransaction(account,account,amount, EnumTypeofTransactions.Credited);
            new JsonReadWrite().WriteData(banks);
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

        public bool Withdraw(string accnum, double amount,string currencyName)
        {
            Account account = FetchAccount(accnum);
            Bank bank = FetchBank(account.BankID);
            currency = GetCurrencyobject(bank, currencyName);
            amount = amount * currency.ExchangeRate;
            if (account.Balance >= amount)
            {
                account.Balance -= amount;
                Transaction NewTransaction = GenerateTransaction(account,account, amount, EnumTypeofTransactions.Debited);
                new JsonReadWrite().WriteData(banks);
                return true;
            }
            else
                throw new InsufficientAmountException();
        }
        public bool TransferAmount(String fromAccNum, string toAccNum, double amount,EnumModeOfTransfer mode,string currencyName )
        {
            Account senderAccount =FetchAccount(fromAccNum);
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
                GenerateTransaction(senderAccount,receiverAccount, amount, EnumTypeofTransactions.Transfer);
                senderBank.BankBalance +=charge;
                new JsonReadWrite().WriteData(banks);
            }
            else
                throw new InsufficientAmountException();
            return true;
        }
        public List<Transaction> DisplayTransactions(string accNum)
        {
            Account account=FetchAccount(accNum);
            return account.Transactions;
        }
        private Transaction GenerateTransaction(Account senderAccount,Account recAccount, Double Amount,EnumTypeofTransactions type)
        {

            Transaction NewTransaction = new Transaction();
            Bank bank = FetchBank(senderAccount.BankID);
            NewTransaction.TransId = $"TXN{bank.BankId.Substring(0, 3)}{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}";
            NewTransaction.Sender = senderAccount.AccountNumber;
            NewTransaction.Receiver =recAccount.AccountNumber;
            NewTransaction.Type = type;
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
        public Bank FetchBank(String bankId)
        {
            foreach (Bank bank in banks)
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


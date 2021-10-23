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
        public bool Deposit(string Accnum, double Amount)
        {
            
            foreach (Bank bank in banks)
            {
                foreach (Account Acc in bank.Accounts)
                {
                    if (Acc.AccountNumber == Accnum)
                    {
                        Acc.Balance += Amount;
                        Transaction NewTransaction = GenerateTransaction(bank, Acc, Amount,"Deposit");
                        bank.BankTransactions.Add(NewTransaction);
                        new JsonReadWrite().WriteData();
                        return true;
                    }
                }
            }
            throw new AccountDoesntExistException();
        }
        public bool Withdraw(string Accnum, double Amount)
        {
            foreach (Bank bank in banks)
            {
                foreach (Account Acc in bank.Accounts)
                {
                    if (Acc.AccountNumber == Accnum)
                    {
                        if (Acc.Balance >= Amount)
                        {
                            Acc.Balance -= Amount;
                            Transaction NewTransaction = GenerateTransaction(bank, Acc, Amount,"Withdrawl");
                            bank.BankTransactions.Add(NewTransaction);
                            new JsonReadWrite().WriteData();
                            return true;
                        }
                        else
                            throw new InsufficientAmountException();
                    }
                }
            }
            throw new AccountDoesntExistException();
        }
        public bool TransferAmount(String FromAccNum, string ToAccNum, double Amount)
        {
            string SendBank = null, RecBank = null;
            foreach (Bank bank in banks)
            {
                foreach (Account Acc in bank.Accounts)
                {
                    if (Acc.AccountNumber == FromAccNum)
                        SendBank = bank.Name;
                    else if (Acc.AccountNumber == ToAccNum)
                        RecBank = bank.Name;
                    if ((SendBank != null) && (RecBank != null))
                    {
                        Withdraw(FromAccNum, Amount);
                        Deposit(ToAccNum, Amount);
                    }
                }
            }
            if (SendBank == null || RecBank == null)
            {
                throw new AccountDoesntExistException();
            }
            return true;
        }
        public List<Transaction> DisplayTransactions(string Accnum)
        {
            foreach (Bank bank in banks)
            {
                foreach (Account Acc in bank.Accounts)
                {
                    if (Acc.AccountNumber == Accnum)
                    {
                        return Acc.Transactions;

                    }
                }
            }
            throw new AccountDoesntExistException();

        }
        private Transaction GenerateTransaction(Bank bank, Account Acc, Double Amount,String type)
        {
            Transaction NewTransaction = new Transaction();
            NewTransaction.TransId = "TXN" + bank.BankId.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            NewTransaction.Sender = "";
            NewTransaction.Receiver = "";
            NewTransaction.Type = type;
            NewTransaction.Amount = Amount;
            Acc.Transactions.Add(NewTransaction);
            return NewTransaction;

        }
    }
}

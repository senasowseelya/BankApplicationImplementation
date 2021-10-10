using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Database;
using BankApp.Models;

namespace BankApp.Services
{
    public class BankServices
    {
        public void  CreateAccountService(Customer newAcc)
        {
            newAcc.accno = GenerateAccountNumber();
            new JsonReadWrite().ReadData();
            new JsonReadWrite().WriteData(newAcc);
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
        public bool DepositAmount(string accnum, double amount)
        {
            new JsonReadWrite().ReadData();
            if (!(Data.Customers.ContainsKey(accnum)))
            {
                throw new AccountDoesntExistException();
            }
            Data.Customers[accnum].balance += amount;
            var today = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " " + amount + " " + "credited";
            Data.Customers[accnum].trans.Add(today);
            new JsonReadWrite().WriteData();
            return true;
        }
        public bool WithdrawMoney(string accnum, double amount)
        {
            new JsonReadWrite().ReadData();
            if (!Data.Customers.ContainsKey(accnum))
            {
                throw new AccountDoesntExistException();
            }

            else if (Data.Customers[accnum].balance < amount)
            {
                throw new InsufficientAmountException();

            }
            else
            {
                Data.Customers[accnum].balance -= amount;
                var today = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " " + amount + " " + "debited";
                Data.Customers[accnum].trans.Add(today);
                new JsonReadWrite().WriteData();
            }
            return true;

        }
        public bool TransferAmount(String fromAccNum, string toAccNum, double amount)
        {
            new JsonReadWrite().ReadData();
            if ((!(Data.Customers.ContainsKey(fromAccNum))) || (!(Data.Customers.ContainsKey(toAccNum))))
            {
                throw new AccountDoesntExistException();
            }
            if (Data.Customers[fromAccNum].balance < amount)
            {
                throw new InsufficientAmountException();
            }
            else
            {

                Data.Customers[toAccNum].balance += amount;
                var today = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " " + amount + " " + "credited";
                Data.Customers[toAccNum].trans.Add(today);
                Data.Customers[fromAccNum].balance -= amount;
                today = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " " + amount + " " + "debited";
                Data.Customers[fromAccNum].trans.Add(today);
                new JsonReadWrite().WriteData();

            }
            return true;
        }
        public List<String> DisplayTransactions(string accnum)
        {
            new JsonReadWrite().ReadData();
            if (!(Data.Customers.ContainsKey(accnum)))
            {
                throw new AccountDoesntExistException();
            }
            return Data.Customers[accnum].trans;
        }
    }
}

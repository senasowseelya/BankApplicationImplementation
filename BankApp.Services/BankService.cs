using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankApp.Database;
using BankApp.Models;


namespace BankApp.Services
{
    public class BankService
    {
        public bool AddBank(String name)
        {
            new JsonReadWrite().ReadData();
            if (Data.Banks.Any(e => e.Name == name))
            {
                throw new BankAlreadyExistsException();
            }

            Bank NewBank = new Bank();
            NewBank.BankId = name.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd"); ;
            NewBank.Name = name;

            new JsonReadWrite().WriteData(NewBank);
            return true;
        }
        public bool CreateAccountService(string BankName, Account NewAcc)
        {
            new JsonReadWrite().ReadData();
            foreach (Bank bank in Data.Banks)
            {
                if (bank.Name == BankName)
                {
                    NewAcc.accno = GenerateAccountNumber();
                    NewAcc.accId = GenerateAccId(BankName);
                    bank.Accounts.Add(NewAcc);
                    new JsonReadWrite().WriteData();

                }
            }
            return true; ;

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
        public bool CheckBank(String BankName)
        {
            new JsonReadWrite().ReadData();
            if (!Data.Banks.Any(e => e.Name == BankName))
                throw new BankDoesntExistException();
            return true;
        }
        public bool DepositAmount(string BankName, string Accnum, double Amount)
        {
            new JsonReadWrite().ReadData();
            foreach (Bank bank in Data.Banks)
            {
                if (bank.Name == BankName)
                {
                    foreach (Account Acc in bank.Accounts)
                    {
                        if (Acc.accno == Accnum)
                        {

                            Acc.balance += Amount;
                            var today = "TXN" + bank.BankId.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd") + " " + Amount + " " + "credited";
                            Acc.trans.Add(today);
                            new JsonReadWrite().WriteData();
                            return true;
                        }
                    }

                }
            }
            throw new AccountDoesntExistException();
        }
        public bool WithdrawMoney(String BankName, string Accnum, double Amount)
        {
            new JsonReadWrite().ReadData();
            foreach (Bank bank in Data.Banks)
            {
                if (bank.Name == BankName)
                {
                    foreach (Account Acc in bank.Accounts)
                    {
                        if (Acc.accno == Accnum)
                        {
                            if (Acc.balance >= Amount)
                            {
                                Acc.balance -= Amount;
                                var today = "TXN" + bank.BankId.Substring(0, 3) + DateTime.Now.ToString("yyyy-MM-dd") + " " + Amount + " " + "Debited";
                                Acc.trans.Add(today);
                                new JsonReadWrite().WriteData();
                                return true;
                            }
                            else
                                throw new InsufficientAmountException();
                        }
                    }
                }
            }
            throw new AccountDoesntExistException();
        }
        public bool TransferAmount(String fromAccNum, string toAccNum, double Amount)
        {
            string SendBank = null, RecBank = null;
            new JsonReadWrite().ReadData();
            foreach (Bank bank in Data.Banks)
            {
                foreach (Account Acc in bank.Accounts)
                {
                    if (Acc.accno == fromAccNum)
                        SendBank = bank.Name;
                    else if (Acc.accno == toAccNum)
                        RecBank = bank.Name;
                    if ((SendBank != null) && (RecBank != null))
                    {
                        WithdrawMoney(SendBank, fromAccNum, Amount);
                        DepositAmount(RecBank, toAccNum, Amount);
                    }
                }
            }
            if (SendBank == null || RecBank == null)
            {
                throw new AccountDoesntExistException();
            }
            return true;
        }

        public List<String> DisplayTransactions(string Accnum)
        {
            new JsonReadWrite().ReadData();
            foreach (Bank bank in Data.Banks)
            {
                foreach (Account Acc in bank.Accounts)
                {
                    if (Acc.accno == Accnum)
                    {
                        return Acc.trans;

                    }
                }
            }
            throw new AccountDoesntExistException();

        }
    }

}
    

    

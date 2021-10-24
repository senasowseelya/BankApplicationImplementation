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
        public bool CreateAccountService(string bankName, Account newAcc)
        {
            new JsonReadWrite().ReadData();
            foreach (Bank bank in banks)
            {
                if (bank.Name == bankName)
                {
                    newAcc.AccountNumber = GenerateAccountNumber();
                    newAcc.AccountId = GenerateAccId(bankName);
                    newAcc.UserName = newAcc.AccholderName.Substring(0,4);
                    newAcc.Password = newAcc.AccountNumber;
                    newAcc.IsActive = true;
                    bank.Accounts.Add(newAcc);
                    new JsonReadWrite().WriteData(banks);
                    return true;

                }
            }
            throw new BankDoesntExistException();
          

        }
        public bool RemoveAccount(String bankName,String accountNumber)
        {
            foreach (Bank bank in banks)
            {
                if (bank.Name == bankName)
                {
                    foreach (Account account in bank.Accounts)
                    {
                        if (account.AccountNumber == accountNumber)
                        {
                            
                            account.IsActive = false;
                            new JsonReadWrite().WriteData(banks);
                            return true;
                        }
                    }

                }
            }
            throw new AccountDoesntExistException();

        }
        
        
        public bool CheckBank(String BankName)
        {
            if (!banks.Any(e => e.Name == BankName))
                throw new BankDoesntExistException();
            return true;
        }
        public bool AddCharges( String bankName, ServiceCharges serviceCharges)
        {
            foreach (Bank bank in banks)
            {
                if (bank.Name == bankName)
                {
                    bank.SelfIMPS = serviceCharges.SelfIMPS;
                    bank.SelfRTGS = serviceCharges.SelfRTGS;
                    bank.OtherIMPS = serviceCharges.OtherIMPS;
                    bank.OtherRTGS = serviceCharges.OtherRTGS;
                    new JsonReadWrite().WriteData(banks);
                    return true;
                }

            }

             return false;
        }
        public List<Transaction> ViewTransaction(String bankName)
        {
            foreach (Bank bank in banks)
            {
                if (bank.Name == bankName)
                {
                    return bank.BankTransactions;
                }
            }
            return null;
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

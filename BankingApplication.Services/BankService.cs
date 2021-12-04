using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankingApplication.Models;

namespace BankingApplication.Services
{
    public class BankService
    {
        public void AddBank(string bankname,string branch)
        {
            BankDataBaseContext dbContext = new BankDataBaseContext();
            Bank newBank = new Bank();
            newBank.name = bankname;
            newBank.balance = 0;
            newBank.branch = branch;
            newBank.ifsc = bankname.Substring(0, bankname.Length - 1) + "567";
            newBank.id=bankname.Substring(0,bankname.Length -1)+"123";
            dbContext.banks.Add(newBank);
            dbContext.SaveChanges();
        }
        public bool AddEmployee(Employee employee)
        {
            return true;
        }

    }
}

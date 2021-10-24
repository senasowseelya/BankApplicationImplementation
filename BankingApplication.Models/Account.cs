using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Account
    { 
        public bool IsActive { get; set; }
        public String BankID { get; set; }
        public String AccholderName { get; set; } 
        public String Nationality { get; set; }
        public String Religion { get; set; }
        public String Category { get; set; }
        public int Age { get; set; }
        public String FathOrHusbName { get; set; }
        public String Type { get; set; }
        public String AccountId { get; set; }
        public String AccountNumber { get; set; }
        public String Dist { get; set; } 
        public String State { get; set; }
        public String Town { get; set; }
        public String Gender { get; set; }
        public String Address { get; set; }
        public String DOB { get; set; }
        public String MaritalStatus { get; set; }
        public String Aadhar { get; set; }
        public String PhoneNumber { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public int Pin { get; set; }
        public Double Balance { get; set; }
        
        public List<Transaction> Transactions=new List<Transaction>();
    }
}

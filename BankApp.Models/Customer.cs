using System;
using System.Collections.Generic;
namespace BankApp

{
    public class Customer
    {
        public string accHoldName, nationality, religion, category, fathOrHusbName, aadhar;
        public string maritalStatus, address, dob, type, gender, town, dist, state, accno;
        public double phno, age, pin;
        public double balance = 0.0;
        public string dateOfIssue;
        public List<String> trans = new List<string>();


    }
}

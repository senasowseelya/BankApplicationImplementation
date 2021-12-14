using System;

namespace BankingApplication.Models
{
    

    public  class Employee
    {
        public string employeeId { get; set; }

        public string bankId { get; set; }

        public string userId { get; set; }

       public  BankUser bankuser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public  class Employee
    {
        public String BankId { get; set; }
        public  String EmpID { get; set; }
        public String Job { get; set; }
        public User User { get; set; }
    }
}

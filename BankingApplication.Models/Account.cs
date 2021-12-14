using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public  class Account
    {
        public string accountNumber  { get; set; }
        public string accountId { get; set; }
        public string bankId { get; set; }

        public string userId { get; set; }
        public string  userName { get; set; }
        public string password { get; set; }
        public decimal balance { get; set; }

        
    }
}

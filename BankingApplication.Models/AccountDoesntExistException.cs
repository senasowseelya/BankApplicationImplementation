using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public  class AccountDoesntExistException:Exception
    {
        public AccountDoesntExistException():base("Account Doesn't Exist"){}
        
    }
}

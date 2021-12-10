using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Services
{
    public  class AccountDoesntExistException:Exception
    {
        public AccountDoesntExistException():base("Account Doesn't Exist"){}
        
    }
}

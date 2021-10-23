using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
     public class InvalidUserException:Exception
    {
        public InvalidUserException():base("Invalid Username or Password Please check again")
        {

        }
    }
}

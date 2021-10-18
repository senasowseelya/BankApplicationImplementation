using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
     public class BankAlreadyExistsException:Exception
    {
        public BankAlreadyExistsException():base("Bank with given name already exists")
        {

        }
    }
}

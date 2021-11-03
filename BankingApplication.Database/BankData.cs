using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Database
{
    public class BankData
    {
        public static List<Bank> banks = new JsonReadWrite().ReadData();
    }
}

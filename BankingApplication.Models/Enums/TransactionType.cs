using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public enum TransactionType
    {
        Credited=1,
        Debited,
        Transfer,
        ServiceCharges,
        Revert,
        Failed

    }
}

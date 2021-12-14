using System;


namespace BankingApplication.Models
{

    public class Transaction
    {
        public string transid { get; set; }

        public string senderaccountId { get; set; }

        public string receiveraccountId { get; set; }

        public DateTime transactionOn { get; set; }

        public decimal transactionAmount { get; set; }

        public string type { get; set; }

        public string  currency { get; set; }
        public Currency currency1 { get; set; }

    }
}

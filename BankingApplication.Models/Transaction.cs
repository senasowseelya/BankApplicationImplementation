namespace BankingApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [Key]
        [StringLength(50)]
        public string transid { get; set; }

        [Required]
        [StringLength(20)]
        public string senderaccountId { get; set; }

        [Required]
        [StringLength(20)]
        public string receiveraccountId { get; set; }

        public DateTime transactionOn { get; set; }

        public decimal transactionAmount { get; set; }

        [StringLength(10)]
        public string type { get; set; }

        [StringLength(20)]
        public string currency { get; set; }

        public virtual Account account { get; set; }

        public virtual Account account1 { get; set; }

        public virtual Currency currency1 { get; set; }

        public virtual TransactionType transactionType { get; set; }
    }
}

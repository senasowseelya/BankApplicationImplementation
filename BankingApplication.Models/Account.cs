namespace BankingApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            transactions = new HashSet<Transaction>();
            transactions1 = new HashSet<Transaction>();
        }

        [StringLength(20)]
        public string accountId { get; set; }

        [StringLength(15)]
        public string bankId { get; set; }

        [Required]
        [StringLength(20)]
        public string accountNumber { get; set; }

        public decimal balance { get; set; }

        [Required]
        [StringLength(10)]
        public string status { get; set; }

        public DateTime dateOfIssue { get; set; }

        [StringLength(20)]
        public string userId { get; set; }

        public virtual Bank bank { get; set; }

        public virtual BankUser bankuser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> transactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> transactions1 { get; set; }
    }
}

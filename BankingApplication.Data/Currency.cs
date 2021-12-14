namespace BankingApplication.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("currency")]
    public partial class Currency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Currency()
        {
            transactions = new HashSet<Transaction>();
        }

        [StringLength(20)]
        public string id { get; set; }

        [StringLength(10)]
        public string name { get; set; }

        public decimal exchangeRate { get; set; }

        [Required]
        [StringLength(15)]
        public string bankid { get; set; }

        public virtual Bank bank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> transactions { get; set; }
    }
}

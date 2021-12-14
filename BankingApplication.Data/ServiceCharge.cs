namespace BankingApplication.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("serviceCharge")]
    public partial class ServiceCharge
    {
        [StringLength(10)]
        public string id { get; set; }

        [Required]
        [StringLength(25)]
        public string name { get; set; }

        public decimal value { get; set; }

        [StringLength(15)]
        public string bankId { get; set; }

        public virtual Bank bank { get; set; }
    }
}

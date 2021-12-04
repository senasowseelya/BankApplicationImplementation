namespace BankingApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employee")]
    public partial class Employee
    {
        [StringLength(20)]
        public string employeeId { get; set; }

        [Required]
        [StringLength(15)]
        public string bankId { get; set; }

        [Required]
        [StringLength(20)]
        public string userId { get; set; }

        public virtual Bank bank { get; set; }

        public virtual BankUser bankuser { get; set; }
    }
}

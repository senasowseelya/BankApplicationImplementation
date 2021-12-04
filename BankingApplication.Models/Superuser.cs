namespace BankingApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Superuser
    {
        [Key]
        [StringLength(20)]
        public string Sid { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(10)]
        public string Password { get; set; }
    }
}

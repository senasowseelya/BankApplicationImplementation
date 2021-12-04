namespace BankingApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bankuser")]
    public partial class BankUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankUser()
        {
            accounts = new HashSet<Account>();
            employees = new HashSet<Employee>();
        }

        [StringLength(20)]
        public string id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        public int age { get; set; }

        [Column(TypeName = "date")]
        public DateTime dob { get; set; }

        [Required]
        [StringLength(10)]
        public string contactNumber { get; set; }

        public long aadharNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string nationality { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> employees { get; set; }
    }
}

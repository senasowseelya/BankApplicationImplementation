using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BankingApplication.Models
{
    public partial class BankDataBaseContext : DbContext
    {
        public BankDataBaseContext()
            : base("name=BankDataBaseContext")
        {
        }

        public virtual DbSet<Account> accounts { get; set; }
        public virtual DbSet<Bank> banks { get; set; }
        public virtual DbSet<BankUser> bankusers { get; set; }
        public virtual DbSet<Currency> currencies { get; set; }
        public virtual DbSet<Employee> employees { get; set; }
        public virtual DbSet<ServiceCharge> serviceCharges { get; set; }
        public virtual DbSet<Transaction> transactions { get; set; }
        public virtual DbSet<TransactionType> transactionTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.accountId)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.bankId)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.accountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.balance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Account>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.userId)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.transactions)
                .WithRequired(e => e.account)
                .HasForeignKey(e => e.receiveraccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.transactions1)
                .WithRequired(e => e.account1)
                .HasForeignKey(e => e.senderaccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.branch)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.ifsc)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.balance)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.currencies)
                .WithRequired(e => e.bank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.employees)
                .WithRequired(e => e.bank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankUser>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<BankUser>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<BankUser>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<BankUser>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<BankUser>()
                .Property(e => e.contactNumber)
                .IsUnicode(false);

            modelBuilder.Entity<BankUser>()
                .Property(e => e.nationality)
                .IsUnicode(false);

            modelBuilder.Entity<BankUser>()
                .HasMany(e => e.accounts)
                .WithOptional(e => e.bankuser)
                .HasForeignKey(e => e.userId);

            modelBuilder.Entity<BankUser>()
                .HasMany(e => e.employees)
                .WithRequired(e => e.bankuser)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.exchangeRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Currency>()
                .Property(e => e.bankid)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.transactions)
                .WithOptional(e => e.currency1)
                .HasForeignKey(e => e.currency);

            modelBuilder.Entity<Employee>()
                .Property(e => e.employeeId)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.bankId)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.userId)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceCharge>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceCharge>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceCharge>()
                .Property(e => e.value)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ServiceCharge>()
                .Property(e => e.bankId)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.transid)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.senderaccountId)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.receiveraccountId)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.transactionAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.currency)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.transactions)
                .WithOptional(e => e.transactionType)
                .HasForeignKey(e => e.type);
        }
    }
}

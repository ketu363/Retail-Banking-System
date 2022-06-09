using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Valsa.Models;

namespace Valsa.Data
{
    public class CreateAccountContext:DbContext
    {
        public CreateAccountContext(DbContextOptions<CreateAccountContext> options):base(options) {}

        public DbSet<CustomerDetail> customerDetails { get; set; }
        public DbSet<Account> accounts { get; set; }

        public DbSet<CustomerLogin> CustomerLogins { get; set; }
        public DbSet<EmployeeLogin> EmployeeLogins { get; set; }
         public virtual DbSet<Employee> Employee { get; set; }
        public DbSet<Loan> loans { get; set; }
        public DbSet<FQ> FQ { get; set; }
        public DbSet<Transaction> transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetail>()
            .HasIndex(m => m.email).IsUnique();

            modelBuilder.Entity<CustomerDetail>()
            .HasIndex(m => m.mobile_number).IsUnique();

            modelBuilder.Entity<CustomerDetail>()
           .HasIndex(m => m.pan_no).IsUnique();

            modelBuilder.Entity<Account>()
                 .HasIndex(m => m.account_number).IsUnique();


        }

    }

}

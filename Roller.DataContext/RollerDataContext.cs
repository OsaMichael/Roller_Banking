using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Roller.DataContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Roller.DContext
{
   public class RollerDataContext : IdentityDbContext<User, Role, int>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public RollerDataContext(DbContextOptions<RollerDataContext> options, IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public RollerDataContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Disposition> Dispositions { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<PermenentOrder> PermenentOrder { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<SupperAccount> SupperAccounts { get; set; }
        
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Branch> Branchs { get; set; }
        public virtual DbSet<BranchManager> BranchManagers { get; set; }
        public virtual DbSet<Cashier> Cashiers { get; set; }
        public virtual DbSet<CheckBook> LoCheckBooks { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<HROfficer> HROfficers { get; set; }

        public virtual DbSet<LoanInfo> LoanInfo { get; set; }
        public virtual DbSet<LoanOfficer> LoanOfficers { get; set; }
        public virtual DbSet<MDirector> MDirectors { get; set; }
        public virtual DbSet<Officer> Officers { get; set; }
        public virtual DbSet<TransferOfficer> TransferOfficers { get; set; }
      
        public DbSet<EmailSentLog> EmailSentLogs { get; set; }
        public DbSet<OTPLog> OTPs { get; set; }
        //public virtual DbSet<StateOfOrigin> StateOfOrigins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Role>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int saved = 0;
           // var currentUser = _contextAccessor.HttpContext.User.Identity.Name;
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<DataContext.BaseEntity.Entity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = currentDate;
                      //  entry.Entity.CreatedBy = currentUser;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.LastDateUpdated = currentDate;
                     //   entry.Entity.UpdatedBy = currentUser;
                    }
                }
                saved = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception Ex)
            {
                //Log.Error(Ex, "An error has occured in SaveChanges");
                //Log.Error(Ex.InnerException, "An error has occured in SaveChanges InnerException");
                //Log.Error(Ex.StackTrace, "An error has occured in SaveChanges StackTrace");
            }

            return saved;
            //   return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            int saved = 0;
            var currentUser = _contextAccessor.HttpContext.User.Identity.Name;
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<DataContext.BaseEntity.Entity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = currentDate;
                        entry.Entity.CreatedBy = currentUser;
                        //entry.Entity.LastDateUpdated = currentDate;
                        //entry.Entity.UpdatedBy = currentUser;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.LastDateUpdated = currentDate;
                        entry.Entity.UpdatedBy = currentUser;
                        //if (entry.Entity.IsDeleted == true && entry.Entity.DateDeleted == null)
                        //{
                        //    entry.Entity.DateDeleted = currentDate;
                        //    entry.Entity.DeletedBy = currentUser;
                        //}
                    }
                }
                saved = base.SaveChanges();
            }
            catch (Exception Ex)
            {
                //Log.Error(Ex, "An error has occured in SaveChanges");
                //Log.Error(Ex.InnerException, "An error has occured in SaveChanges InnerException");
                //Log.Error(Ex.StackTrace, "An error has occured in SaveChanges StackTrace");
                throw Ex;
            }

            return saved;
        }

        public async  Task<int> Saved()
        {
            int saved = 0;
           // var currentUser = _contextAccessor.HttpContext.User.Identity.Name;
            var currentDate = DateTime.Now;
            try
            {
                foreach (var entry in ChangeTracker.Entries<DataContext.BaseEntity.Entity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = currentDate;
                       // entry.Entity.CreatedBy = currentUser;
                        //entry.Entity.LastDateUpdated = currentDate;
                        //entry.Entity.UpdatedBy = currentUser;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.LastDateUpdated = currentDate;
                       // entry.Entity.UpdatedBy = currentUser;
                        //if (entry.Entity.IsDeleted == true && entry.Entity.DateDeleted == null)
                        //{
                        //    entry.Entity.DateDeleted = currentDate;
                        //    entry.Entity.DeletedBy = currentUser;
                        //}
                    }
                }
                saved = await base.SaveChangesAsync();
            }
            catch (Exception Ex)
            {
                //Log.Error(Ex, "An error has occured in SaveChanges");
                //Log.Error(Ex.InnerException, "An error has occured in SaveChanges InnerException");
                //Log.Error(Ex.StackTrace, "An error has occured in SaveChanges StackTrace");
                throw Ex;
            }

            return saved;
        }

        public class ContextDesignFactory : IDesignTimeDbContextFactory<RollerDataContext>
        {
            public RollerDataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RollerDataContext>()
                .UseSqlServer("Server=DESKTOP-9IAAL5C;Database=RollerBank;User ID=sa;Password=Test_test1;Trusted_Connection=False; MultipleActiveResultSets=true;");
                //.UseSqlServer("Data Source=TEMP053-SOFT-01\\SQLEXPRESS;Initial Catalog=RollerApp;Integrated Security=True");
                

                return new RollerDataContext(optionsBuilder.Options);
            }
        }

    }
}

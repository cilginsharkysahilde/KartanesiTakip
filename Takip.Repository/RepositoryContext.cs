using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takip.Model.Tables;
using Takip.Model.Views;

namespace Takip.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) 
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<V_AdminCategories>().HasNoKey();
            modelBuilder.Entity<V_AdminCustomers>().HasNoKey();
            modelBuilder.Entity<V_AdminAddresses>().HasNoKey();
            modelBuilder.Entity<V_AdminServices>().HasNoKey();
            modelBuilder.Entity<V_AdminPersonnelDetail>().HasNoKey();
            modelBuilder.Entity<Personnel>().Property(d => d.StartDate).HasDefaultValue();
            modelBuilder.Entity<Customer>().Property(d => d.RegistryDate).HasDefaultValue();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProcessType> ProcessTypes { get; set; }
        public DbSet<EducationalBackground> EducationalBackgrounds { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Country> Countries { get; set; }


        public DbSet<V_ActivePersonnels> ActivePersonnels { get; set; }
        public DbSet<V_AdminCategories> AdminCategories { get; set; }
        public DbSet<V_AdminCustomers> AdminCustomers { get; set; }
        public DbSet<V_AdminAddresses> AdminAddresses { get; set; }
        public DbSet<V_Product> V_Products { get; set; }
        public DbSet<V_Personnel> V_Personnels { get; set; }
        public DbSet<V_Customer> V_Customers { get; set; }
        public DbSet<V_AdminServices> V_AdminServices { get; set; }
        public DbSet<V_AdminPersonnelDetail> V_AdminPersonnelDetails { get; set; }

    }
}

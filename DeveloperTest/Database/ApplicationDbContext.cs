using System;
using Microsoft.EntityFrameworkCore;
using DeveloperTest.Database.Models;

namespace DeveloperTest.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        public DbSet<CustomerType> CustomerTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasKey(x => x.JobId);

            modelBuilder.Entity<Job>()
                .Property(x => x.JobId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Job>()
                .HasData(new Job
                {
                    JobId = 1,
                    Engineer = "Test",
                    When = DateTime.Now
                });

            modelBuilder.Entity<Job>()
                .HasOne(e => e.Customer)
                .WithMany(c => c.Jobs)
                .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<CustomerType>()
                .HasKey(x => x.CustomerTypeId);

            modelBuilder.Entity<CustomerType>()
                .Property(x => x.CustomerTypeId)
                .ValueGeneratedNever();

            modelBuilder.Entity<CustomerType>()
                .HasData(new CustomerType
                {
                    CustomerTypeId = 1,
                    Name = "Large"
                },
                new CustomerType
                {
                    CustomerTypeId = 2,
                    Name = "Small"
                }
            );

            modelBuilder.Entity<Customer>()
                .HasKey(x => x.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.TypeId)
                .IsRequired();
        }
    }
}

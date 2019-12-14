using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApiLabor.Bll.Entities;

namespace WebApiLabor.Bll.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(c => c.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.ProductOrders)
                .WithOne(po => po.Order)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

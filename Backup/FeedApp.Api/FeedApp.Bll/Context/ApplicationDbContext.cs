using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedApp.Bll.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<Eating>().ToTable("Eating");
            modelBuilder.Entity<FoodsForEating>().ToTable("FoodsForEating");
            modelBuilder.Entity<DailyWeightInfo>().ToTable("DailyWeightInfo");
            //modelBuilder.Entity<DailyConsumption>().ToTable("DailyConsumption");
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Eating> Eatings { get; set; }
        public DbSet<FoodsForEating> FoodsForEatings { get; set; }
        public DbSet<DailyWeightInfo> DailyWeightInfos { get; set; }
        //public DbSet<DailyConsumption> DailyConsumptions { get; set; }
    }

}

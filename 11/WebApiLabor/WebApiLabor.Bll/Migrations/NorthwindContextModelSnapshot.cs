using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApiLabor.Bll.Context;
using WebApiLabor.Bll.Entities;

namespace WebApiLabor.Bll.Migrations
{
    [DbContext(typeof(NorthwindContext))]
    partial class NorthwindContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiLabor.Bll.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebApiLabor.Bll.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("OrderDate");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApiLabor.Bll.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.Property<int>("ShipmentRegion");

                    b.Property<int>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApiLabor.Bll.Entities.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductOrder");
                });

            modelBuilder.Entity("WebApiLabor.Bll.Entities.Product", b =>
                {
                    b.HasOne("WebApiLabor.Bll.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApiLabor.Bll.Entities.ProductOrder", b =>
                {
                    b.HasOne("WebApiLabor.Bll.Entities.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId");

                    b.HasOne("WebApiLabor.Bll.Entities.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

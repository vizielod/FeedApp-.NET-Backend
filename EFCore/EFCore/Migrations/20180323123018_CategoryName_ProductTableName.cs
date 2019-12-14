using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EFCore.Migrations
{
    public partial class CategoryName_ProductTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "PRODUCT");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "PRODUCT",
                newName: "IX_PRODUCT_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCT",
                table: "PRODUCT",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_Categories_CategoryId",
                table: "PRODUCT",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_PRODUCT_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "PRODUCT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_Categories_CategoryId",
                table: "PRODUCT");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_PRODUCT_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCT",
                table: "PRODUCT");

            migrationBuilder.RenameTable(
                name: "PRODUCT",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCT_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

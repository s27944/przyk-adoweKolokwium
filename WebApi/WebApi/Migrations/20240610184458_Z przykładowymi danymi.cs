using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Zprzykładowymidanymi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[] { 1, "Jacek", "Sasin" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Name", "Price" },
                values: new object[] { 1, "Czekolada", 5m });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Utworzone" },
                    { 2, "Zrealizowane" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "ID", "ClientID", "CreatedAt", "FulfilledAt", "StatusID" },
                values: new object[] { 1, 1, new DateTime(2024, 6, 10, 20, 44, 57, 731, DateTimeKind.Local).AddTicks(7431), null, 1 });

            migrationBuilder.InsertData(
                table: "Product_Order",
                columns: new[] { "OrderID", "ProductID", "Amount" },
                values: new object[] { 1, 1, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product_Order",
                keyColumns: new[] { "OrderID", "ProductID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}

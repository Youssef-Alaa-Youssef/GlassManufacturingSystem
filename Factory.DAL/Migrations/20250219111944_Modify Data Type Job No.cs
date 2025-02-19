using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Factory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDataTypeJobNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<long>(
                name: "JobNo",
                table: "Order",
                type: "bigint",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Items",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Thickness",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Items",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Items",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFragile",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "Items",
                type: "nvarchar(max)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "IconClass", "Name", "Url" },
                values: new object[,]
                {
                    { 4, "", "Warehouse Management", "/Warehouse/Index" },
                    { 5, "", "Order Management", "/Order/Index" }
                });

            migrationBuilder.InsertData(
                table: "SubModules",
                columns: new[] { "Id", "Action", "Controller", "ModuleId", "Name", "Title" },
                values: new object[,]
                {
                    { 7, "index", "Warehouse", 4, "Warehouse Management", "Warehouse Management" },
                    { 8, "index", "Item", 4, "Item Management", "Item Management" },
                    { 9, "Create", "Order", 5, "Order Management", "Order Management" },
                    { 10, "index", "Order", 5, "Order Management", "Order Management" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubModules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SubModules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SubModules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SubModules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "JobNo",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Thickness",
                table: "Items",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Items",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Items",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFragile",
                table: "Items",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Color", "Description", "Dimensions", "ExpiryDate", "IsFragile", "ManufactureDate", "Manufacturer", "Name", "Notes", "Quantity", "SubWarehouseId", "Thickness", "Type", "UnitPrice", "WarehouseId" },
                values: new object[,]
                {
                    { 1, "Clear", "Transparent glass panel", "1200x800mm", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GlassCo", "Glass Panel", "Handle with care", 100, 1, "5mm", "Panel", 50.00m, 1 },
                    { 2, "Tinted", "Strong tempered glass", "1500x1000mm", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TemperedGlassCo", "Tempered Glass", "Heat-resistant", 50, 2, "10mm", "Tempered", 100.00m, 1 }
                });
        }
    }
}

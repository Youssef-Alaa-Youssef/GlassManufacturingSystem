using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adddeliverycolumnsinorderitemmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveredBy",
                table: "OrderItem",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "OrderItem",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "OrderItem",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredBy",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "OrderItem");
        }
    }
}

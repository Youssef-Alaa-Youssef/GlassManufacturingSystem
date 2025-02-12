using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Factory.DAL.Migrations
{
    /// <inheritdoc />
    public partial class OrderApplied : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SelectedMachines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSQM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SQM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSQM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}

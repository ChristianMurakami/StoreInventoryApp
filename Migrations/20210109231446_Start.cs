using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreInventoryApplication.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProdId_N_Counts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ProdCount = table.Column<int>(nullable: false),
                    ProdName = table.Column<string>(nullable: true),
                    LocationKey = table.Column<string>(nullable: true),
                    LocationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdId_N_Counts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Assigned = table.Column<bool>(nullable: false),
                    Vendor = table.Column<string>(nullable: true),
                    CountPerCase = table.Column<int>(nullable: false),
                    Constraint = table.Column<int>(nullable: false),
                    InventoryCost = table.Column<decimal>(nullable: false),
                    PricePerItem = table.Column<decimal>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    FinalTotal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AisleId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    ProdCount = table.Column<int>(nullable: false),
                    Constraint = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreOrders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StoreId = table.Column<string>(nullable: true),
                    InventoryCost = table.Column<decimal>(nullable: false),
                    Submitted = table.Column<bool>(nullable: false),
                    DateToSubmit = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdId_N_Counts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropTable(
                name: "StoreOrders");
        }
    }
}

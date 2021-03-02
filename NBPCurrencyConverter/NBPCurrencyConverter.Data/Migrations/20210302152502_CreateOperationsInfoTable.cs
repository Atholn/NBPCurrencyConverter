using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBPCurrencyConverter.Data.Migrations
{
    public partial class CreateOperationsInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCodeFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyCodeTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyMidFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyMidTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsInfo");
        }
    }
}

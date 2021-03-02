using Microsoft.EntityFrameworkCore.Migrations;

namespace NBPCurrencyConverter.Data.Migrations
{
    public partial class UpdateOperationsInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Result",
                table: "OperationsInfo",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "OperationsInfo");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriptoBiyuya.Migrations
{
    /// <inheritdoc />
    public partial class transactionaddMoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "money",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "money",
                table: "Transactions");
        }
    }
}

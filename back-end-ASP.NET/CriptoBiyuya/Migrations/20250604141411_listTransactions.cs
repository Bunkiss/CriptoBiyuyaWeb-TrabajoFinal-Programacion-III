using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriptoBiyuya.Migrations
{
    /// <inheritdoc />
    public partial class listTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Clients_clientid",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "clientid",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Clients_clientid",
                table: "Transactions",
                column: "clientid",
                principalTable: "Clients",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Clients_clientid",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "clientid",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Clients_clientid",
                table: "Transactions",
                column: "clientid",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

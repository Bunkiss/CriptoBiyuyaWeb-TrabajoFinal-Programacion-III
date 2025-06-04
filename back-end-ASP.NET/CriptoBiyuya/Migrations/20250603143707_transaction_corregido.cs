using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriptoBiyuya.Migrations
{
    /// <inheritdoc />
    public partial class transaction_corregido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    crypto_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    crypto_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    clientid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transactions_Clients_clientid",
                        column: x => x.clientid,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_clientid",
                table: "Transactions",
                column: "clientid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}

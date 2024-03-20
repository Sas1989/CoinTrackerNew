using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinList.Infrastrcture.Migrations
{
    /// <inheritdoc />
    public partial class CoinListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CoinList");

            migrationBuilder.CreateTable(
                name: "Coin",
                schema: "CoinList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.Id);
                    table.CheckConstraint("CK_CoinList_PriceNegative", "Price >= 0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coin_Symbol",
                schema: "CoinList",
                table: "Coin",
                column: "Symbol",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coin",
                schema: "CoinList");
        }
    }
}

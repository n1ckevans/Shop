using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class RenameStockOnHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOnHolds_Stock_StockId",
                table: "StockOnHolds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOnHolds",
                table: "StockOnHolds");

            migrationBuilder.RenameTable(
                name: "StockOnHolds",
                newName: "StockOnHold");

            migrationBuilder.RenameIndex(
                name: "IX_StockOnHolds_StockId",
                table: "StockOnHold",
                newName: "IX_StockOnHold_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOnHold",
                table: "StockOnHold",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOnHold_Stock_StockId",
                table: "StockOnHold",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOnHold_Stock_StockId",
                table: "StockOnHold");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOnHold",
                table: "StockOnHold");

            migrationBuilder.RenameTable(
                name: "StockOnHold",
                newName: "StockOnHolds");

            migrationBuilder.RenameIndex(
                name: "IX_StockOnHold_StockId",
                table: "StockOnHolds",
                newName: "IX_StockOnHolds_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOnHolds",
                table: "StockOnHolds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockOnHolds_Stock_StockId",
                table: "StockOnHolds",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

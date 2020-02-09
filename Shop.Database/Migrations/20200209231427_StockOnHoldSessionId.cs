using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class StockOnHoldSessionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "StockOnHold",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "StockOnHold");
        }
    }
}

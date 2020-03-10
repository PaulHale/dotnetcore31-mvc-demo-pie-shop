using Microsoft.EntityFrameworkCore.Migrations;

namespace pieshop.Migrations
{
    public partial class OrderDetailNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "OrderDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "OrderDetails");
        }
    }
}

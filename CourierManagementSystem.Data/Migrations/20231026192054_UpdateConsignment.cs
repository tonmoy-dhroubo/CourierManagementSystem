using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierManagementSystem.Data.Migrations
{
    public partial class UpdateConsignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Consignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Consignments");
        }
    }
}

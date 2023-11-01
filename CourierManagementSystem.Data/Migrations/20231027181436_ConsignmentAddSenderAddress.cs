using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierManagementSystem.Data.Migrations
{
    public partial class ConsignmentAddSenderAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TrackingLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderAddress",
                table: "Consignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "TrackingLog");

            migrationBuilder.DropColumn(
                name: "SenderAddress",
                table: "Consignments");
        }
    }
}

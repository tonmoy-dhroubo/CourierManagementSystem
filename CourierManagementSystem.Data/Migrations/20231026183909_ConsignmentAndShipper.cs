using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierManagementSystem.Data.Migrations
{
    public partial class ConsignmentAndShipper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParcelWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParcelContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentCompletion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackingLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsignmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingLog_Consignments_ConsignmentId",
                        column: x => x.ConsignmentId,
                        principalTable: "Consignments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackingLog_ConsignmentId",
                table: "TrackingLog",
                column: "ConsignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "TrackingLog");

            migrationBuilder.DropTable(
                name: "Consignments");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiclesAndDrivers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Initials = table.Column<string>(type: "text", nullable: false),
                    LicenseNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WaybillNumber = table.Column<string>(type: "text", nullable: false),
                    QRCode = table.Column<string>(type: "text", nullable: false),
                    DispatchedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DispatchTrackingCode = table.Column<string>(type: "text", nullable: true),
                    Sender = table.Column<string>(type: "text", nullable: false),
                    SenderTelephone = table.Column<string>(type: "text", nullable: false),
                    Receiver = table.Column<string>(type: "text", nullable: false),
                    ReceiverTelephone = table.Column<string>(type: "text", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethods = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(type: "text", nullable: false),
                    BodyType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}

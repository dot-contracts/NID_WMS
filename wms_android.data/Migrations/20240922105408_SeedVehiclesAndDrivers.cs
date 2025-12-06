using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class SeedVehiclesAndDrivers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("15f4ffe1-9f69-4798-bd73-e8f8739d3cf5"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("26a80a6d-4817-4fb6-a113-8dfd4e26bb9a"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("2f42eb92-e3d0-4b49-ad62-90999f2980f2"), "Charles", "SK", "Maina", "DL123456" },
                    { new Guid("857897ac-a7b6-4f75-8229-77fc38808c04"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("a5bcf466-4696-4534-a345-db501b67d096"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("aa7abb8e-f632-4254-9070-db208cb12b7c"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("c1dac1c3-20a2-4ca3-8452-9d0a32ea84c0"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("c351b969-090a-4031-9515-504f1567d2da"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("ca5f29b0-d7bf-4d92-8a8c-9b72326a07be"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("ea10afc4-3c6f-40d2-b4f6-923c125a7ecc"), "David", "DM", "Mwangi", "DL123456" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("070e0332-2634-4bfd-9a18-766bec5331d2"), "Truck", "KDB 387Q" },
                    { new Guid("1725748f-3d59-4bf3-8c46-22e11aba473f"), "Truck", "KCY 067A" },
                    { new Guid("4ca28c4a-36d4-463f-abd8-9703056df7f1"), "Truck", "KDL 085M" },
                    { new Guid("5d044fda-f687-407c-839c-e2660d4e2968"), "Truck", "KDE 228S" },
                    { new Guid("7d23bd8d-a2d5-4b06-a59e-eae9fbab5ca4"), "Van", "KAY 215H" },
                    { new Guid("a2de3132-6cf4-4bad-ab5d-61e63a53de24"), "Van", "KBF 462A" },
                    { new Guid("b9f9530b-bb64-4bba-a327-b1c54b610a21"), "Truck", "KCZ 595L" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("15f4ffe1-9f69-4798-bd73-e8f8739d3cf5"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("26a80a6d-4817-4fb6-a113-8dfd4e26bb9a"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("2f42eb92-e3d0-4b49-ad62-90999f2980f2"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("857897ac-a7b6-4f75-8229-77fc38808c04"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("a5bcf466-4696-4534-a345-db501b67d096"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("aa7abb8e-f632-4254-9070-db208cb12b7c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c1dac1c3-20a2-4ca3-8452-9d0a32ea84c0"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c351b969-090a-4031-9515-504f1567d2da"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ca5f29b0-d7bf-4d92-8a8c-9b72326a07be"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ea10afc4-3c6f-40d2-b4f6-923c125a7ecc"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("070e0332-2634-4bfd-9a18-766bec5331d2"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("1725748f-3d59-4bf3-8c46-22e11aba473f"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("4ca28c4a-36d4-463f-abd8-9703056df7f1"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("5d044fda-f687-407c-839c-e2660d4e2968"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("7d23bd8d-a2d5-4b06-a59e-eae9fbab5ca4"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("a2de3132-6cf4-4bad-ab5d-61e63a53de24"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b9f9530b-bb64-4bba-a327-b1c54b610a21"));
        }
    }
}

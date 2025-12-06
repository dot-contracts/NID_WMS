using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("0fe282a3-0ac8-4bb6-9bde-b2df10fc0d66"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("2d4e41bc-fcbc-43f0-abbb-9452b0e9c8cf"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("2ff0c997-1703-494c-866c-6e934dce5218"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("36c1e080-c15d-4322-868e-7388b9c048b5"), "Charles", "SK", "Maina", "DL123456" },
                    { new Guid("43567c1b-d342-4611-bfac-f0cee0936ebd"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("49268fb0-7800-4721-ac00-6e5e8d354486"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("85926252-bbb7-4e38-bf64-742d439f43f0"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("aa939e80-1058-403a-afed-7ca649169913"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("c5df6acf-6555-48dc-96be-16580ebf6796"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("c6667976-b2bc-4653-9683-8320f3424373"), "Stephen", "SK", "Kimuyu", "DL123456" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator", "Admin" },
                    { 2, "Manager", "Manager" },
                    { 3, "Regular User", "User" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("2a15a41e-7e38-4ab8-9704-b7a947a4696d"), "Truck", "KDB 387Q" },
                    { new Guid("2f36648b-a525-4944-af06-0a3f7d5aa071"), "Truck", "KDL 085M" },
                    { new Guid("3a761dd0-3592-4da7-bef2-3877f37ebf4e"), "Truck", "KCY 067A" },
                    { new Guid("6f143091-2968-418b-8d4a-dcc7d4f55974"), "Van", "KBF 462A" },
                    { new Guid("8feea2de-ce84-4e73-9b5a-5ca657807599"), "Van", "KAY 215H" },
                    { new Guid("ed5967c9-38d9-42fa-8b6c-ed1f12025e3e"), "Truck", "KCZ 595L" },
                    { new Guid("fdb86069-ffea-4f04-89cb-21aeac2a0977"), "Truck", "KDE 228S" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "admin", 1, "admin" },
                    { 2, "manager", 2, "manager" },
                    { 3, "user", 3, "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("0fe282a3-0ac8-4bb6-9bde-b2df10fc0d66"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("2d4e41bc-fcbc-43f0-abbb-9452b0e9c8cf"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("2ff0c997-1703-494c-866c-6e934dce5218"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("36c1e080-c15d-4322-868e-7388b9c048b5"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("43567c1b-d342-4611-bfac-f0cee0936ebd"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("49268fb0-7800-4721-ac00-6e5e8d354486"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("85926252-bbb7-4e38-bf64-742d439f43f0"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("aa939e80-1058-403a-afed-7ca649169913"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c5df6acf-6555-48dc-96be-16580ebf6796"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c6667976-b2bc-4653-9683-8320f3424373"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("2a15a41e-7e38-4ab8-9704-b7a947a4696d"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("2f36648b-a525-4944-af06-0a3f7d5aa071"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("3a761dd0-3592-4da7-bef2-3877f37ebf4e"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6f143091-2968-418b-8d4a-dcc7d4f55974"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("8feea2de-ce84-4e73-9b5a-5ca657807599"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ed5967c9-38d9-42fa-8b6c-ed1f12025e3e"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fdb86069-ffea-4f04-89cb-21aeac2a0977"));

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
    }
}

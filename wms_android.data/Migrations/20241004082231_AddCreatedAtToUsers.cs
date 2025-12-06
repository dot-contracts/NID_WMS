using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("051a132c-191f-4012-8079-0e4c55ea11c0"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("067046cc-8110-4407-9eb7-bf78120a2d1a"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("188ce499-ead7-46fb-997b-af90a92822dd"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("317b39f8-e7d9-43ab-8e4c-4e38c323ac2c"), "Charles", "SK", "Maina", "DL123456" },
                    { new Guid("34b77566-6758-4c1f-845d-c3d1a89eff66"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("5287ded1-44ee-4a8b-aa27-a22328403d3f"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("7ad873a1-4009-4ae7-9895-88ea4aa1b588"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("cf2b56dd-c2fa-40aa-94ff-1c93b514854b"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("f983af0e-3542-4554-bdb3-ef6d91a89bce"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("f9cb1fb0-10f3-45ec-a92b-c53307cccc8a"), "John", "JM", "Mwai", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Clerk");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "Client User", "Client" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Email", "Password", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@example.com", "admin123", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Email", "Password", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "manager@example.com", "manager123", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Email", "Password", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "clerk1@example.com", "clerk123", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "clerk1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "PasswordHash", "PasswordSalt", "RoleId", "Username" },
                values: new object[] { 4, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "clerk2@example.com", "clerk123", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 3, "clerk2" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("33d802a6-79c3-4901-a710-717c9ea5c6e2"), "Truck", "KDB 387Q" },
                    { new Guid("3dc1da49-a6d4-4f0f-8e4e-a8dedbe567dc"), "Truck", "KCY 067A" },
                    { new Guid("72554e63-d5ce-4ab0-9be5-5293350e89c6"), "Van", "KAY 215H" },
                    { new Guid("946a2ebc-2bf1-465f-b2cc-954b0451e515"), "Van", "KBF 462A" },
                    { new Guid("a6863f38-0603-4feb-95f1-afd28387496a"), "Truck", "KCZ 595L" },
                    { new Guid("b312c0a7-fa5b-4968-abf8-acf209bc38a8"), "Truck", "KDE 228S" },
                    { new Guid("d326c173-d1cd-4ab5-89fe-6dbd3d28252a"), "Truck", "KDL 085M" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "PasswordHash", "PasswordSalt", "RoleId", "Username" },
                values: new object[] { 5, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "client1@example.com", "client123", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 4, "client1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("051a132c-191f-4012-8079-0e4c55ea11c0"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("067046cc-8110-4407-9eb7-bf78120a2d1a"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("188ce499-ead7-46fb-997b-af90a92822dd"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("317b39f8-e7d9-43ab-8e4c-4e38c323ac2c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("34b77566-6758-4c1f-845d-c3d1a89eff66"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("5287ded1-44ee-4a8b-aa27-a22328403d3f"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("7ad873a1-4009-4ae7-9895-88ea4aa1b588"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("cf2b56dd-c2fa-40aa-94ff-1c93b514854b"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f983af0e-3542-4554-bdb3-ef6d91a89bce"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f9cb1fb0-10f3-45ec-a92b-c53307cccc8a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("33d802a6-79c3-4901-a710-717c9ea5c6e2"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("3dc1da49-a6d4-4f0f-8e4e-a8dedbe567dc"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("72554e63-d5ce-4ab0-9be5-5293350e89c6"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("946a2ebc-2bf1-465f-b2cc-954b0451e515"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("a6863f38-0603-4feb-95f1-afd28387496a"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b312c0a7-fa5b-4968-abf8-acf209bc38a8"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d326c173-d1cd-4ab5-89fe-6dbd3d28252a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

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

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "User");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "manager");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Username" },
                values: new object[] { "user", "user" });

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
        }
    }
}

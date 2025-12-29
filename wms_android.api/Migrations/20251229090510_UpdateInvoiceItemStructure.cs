using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceItemStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("01e46104-5571-4357-adf2-26805ff5d003"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("2799bf53-ebd5-439f-a65a-6bd10adcf156"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("614cea0a-b928-4e9d-be69-996807d268f9"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("6ead1e5a-9181-41e3-9085-261a6190bb6a"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("6f631350-e3ea-490b-8661-fb0440e22e09"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("813c59b8-3ae2-4ae6-8858-07e05b4e68e8"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8838002d-f7fe-4d6c-9dd5-3264de7caf75"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b7b87f9a-fed6-4096-bb3e-e0d4a369e5e6"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d77ec151-bf52-4771-bbd9-d449d2628a42"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f44c7939-20ea-4ce9-bbd9-0adb6fe59818"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("1808b1fa-7bf1-44ac-9976-ac4fdcc83fc8"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("190e3a46-3719-43f2-8a6c-ca6642986634"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("52ecddc8-2aa8-4816-8312-75707ff22832"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("7c350bbd-9781-419b-b581-f52990410cd3"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("7dfc1d4f-724c-41a5-8fa4-60590c68c8b5"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("888c1d4c-ff95-4fe6-8aa9-7b43e866ea17"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("e32a6d10-b5c4-4782-bcaf-f9414a6c1dec"));

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "InvoiceItems",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ParcelCreatedAt",
                table: "InvoiceItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("0dfbb9c6-5499-4e54-940c-67ec9dbeb7be"), "Charles", "CM", "Maina", "DL123456" },
                    { new Guid("4b2b392c-33db-4865-9be2-bd1b9df250e3"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("60ef3dab-bd2e-421d-9977-2c81e9470405"), "James", "JG", "Gichohi", "DL123456" },
                    { new Guid("63756509-979b-494b-9e66-e96a4e58af17"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("6496dd47-95e6-497f-a48d-d213a3fbcb07"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("ab94ee29-c601-4658-8fa7-d35e098c6eed"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("ad37940d-3460-4f29-a78e-9cc302b2182b"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("aee08f71-fce3-45f4-9d2b-60b94d4d345c"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("de86d3eb-707f-47e2-8ea9-7f75d3ce7126"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("eab8076f-5889-43fb-b7d7-bd76e8d8a42f"), "Erastus", "EK", "Kagwa", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 85, 28, 91, 189, 249, 182, 113, 154, 180, 243, 130, 182, 111, 209, 186, 160, 32, 142, 36, 35, 35, 246, 171, 100, 36, 210, 170, 3, 32, 235, 119, 216, 83, 132, 47, 194, 66, 220, 182, 179, 244, 150, 84, 249, 141, 114, 36, 5, 37, 162, 32, 38, 171, 88, 248, 104, 26, 131, 158, 151, 102, 130, 161 }, new byte[] { 120, 54, 33, 190, 131, 59, 144, 245, 159, 199, 250, 137, 160, 47, 56, 114, 141, 230, 192, 69, 141, 2, 173, 197, 236, 30, 157, 83, 146, 18, 52, 122, 175, 79, 130, 21, 244, 179, 110, 254, 68, 74, 214, 132, 18, 230, 12, 70, 228, 76, 224, 129, 179, 68, 250, 37, 171, 43, 34, 162, 200, 96, 231, 246, 29, 154, 164, 201, 118, 119, 142, 185, 33, 242, 223, 77, 145, 204, 116, 222, 134, 41, 39, 150, 103, 205, 166, 46, 151, 24, 93, 73, 216, 99, 215, 216, 88, 36, 8, 56, 101, 88, 18, 7, 148, 142, 177, 219, 168, 66, 42, 41, 82, 118, 6, 35, 211, 90, 212, 209, 129, 137, 95, 172, 40, 13, 144, 161 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 85, 28, 91, 189, 249, 182, 113, 154, 180, 243, 130, 182, 111, 209, 186, 160, 32, 142, 36, 35, 35, 246, 171, 100, 36, 210, 170, 3, 32, 235, 119, 216, 83, 132, 47, 194, 66, 220, 182, 179, 244, 150, 84, 249, 141, 114, 36, 5, 37, 162, 32, 38, 171, 88, 248, 104, 26, 131, 158, 151, 102, 130, 161 }, new byte[] { 120, 54, 33, 190, 131, 59, 144, 245, 159, 199, 250, 137, 160, 47, 56, 114, 141, 230, 192, 69, 141, 2, 173, 197, 236, 30, 157, 83, 146, 18, 52, 122, 175, 79, 130, 21, 244, 179, 110, 254, 68, 74, 214, 132, 18, 230, 12, 70, 228, 76, 224, 129, 179, 68, 250, 37, 171, 43, 34, 162, 200, 96, 231, 246, 29, 154, 164, 201, 118, 119, 142, 185, 33, 242, 223, 77, 145, 204, 116, 222, 134, 41, 39, 150, 103, 205, 166, 46, 151, 24, 93, 73, 216, 99, 215, 216, 88, 36, 8, 56, 101, 88, 18, 7, 148, 142, 177, 219, 168, 66, 42, 41, 82, 118, 6, 35, 211, 90, 212, 209, 129, 137, 95, 172, 40, 13, 144, 161 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 85, 28, 91, 189, 249, 182, 113, 154, 180, 243, 130, 182, 111, 209, 186, 160, 32, 142, 36, 35, 35, 246, 171, 100, 36, 210, 170, 3, 32, 235, 119, 216, 83, 132, 47, 194, 66, 220, 182, 179, 244, 150, 84, 249, 141, 114, 36, 5, 37, 162, 32, 38, 171, 88, 248, 104, 26, 131, 158, 151, 102, 130, 161 }, new byte[] { 120, 54, 33, 190, 131, 59, 144, 245, 159, 199, 250, 137, 160, 47, 56, 114, 141, 230, 192, 69, 141, 2, 173, 197, 236, 30, 157, 83, 146, 18, 52, 122, 175, 79, 130, 21, 244, 179, 110, 254, 68, 74, 214, 132, 18, 230, 12, 70, 228, 76, 224, 129, 179, 68, 250, 37, 171, 43, 34, 162, 200, 96, 231, 246, 29, 154, 164, 201, 118, 119, 142, 185, 33, 242, 223, 77, 145, 204, 116, 222, 134, 41, 39, 150, 103, 205, 166, 46, 151, 24, 93, 73, 216, 99, 215, 216, 88, 36, 8, 56, 101, 88, 18, 7, 148, 142, 177, 219, 168, 66, 42, 41, 82, 118, 6, 35, 211, 90, 212, 209, 129, 137, 95, 172, 40, 13, 144, 161 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 85, 28, 91, 189, 249, 182, 113, 154, 180, 243, 130, 182, 111, 209, 186, 160, 32, 142, 36, 35, 35, 246, 171, 100, 36, 210, 170, 3, 32, 235, 119, 216, 83, 132, 47, 194, 66, 220, 182, 179, 244, 150, 84, 249, 141, 114, 36, 5, 37, 162, 32, 38, 171, 88, 248, 104, 26, 131, 158, 151, 102, 130, 161 }, new byte[] { 120, 54, 33, 190, 131, 59, 144, 245, 159, 199, 250, 137, 160, 47, 56, 114, 141, 230, 192, 69, 141, 2, 173, 197, 236, 30, 157, 83, 146, 18, 52, 122, 175, 79, 130, 21, 244, 179, 110, 254, 68, 74, 214, 132, 18, 230, 12, 70, 228, 76, 224, 129, 179, 68, 250, 37, 171, 43, 34, 162, 200, 96, 231, 246, 29, 154, 164, 201, 118, 119, 142, 185, 33, 242, 223, 77, 145, 204, 116, 222, 134, 41, 39, 150, 103, 205, 166, 46, 151, 24, 93, 73, 216, 99, 215, 216, 88, 36, 8, 56, 101, 88, 18, 7, 148, 142, 177, 219, 168, 66, 42, 41, 82, 118, 6, 35, 211, 90, 212, 209, 129, 137, 95, 172, 40, 13, 144, 161 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 85, 28, 91, 189, 249, 182, 113, 154, 180, 243, 130, 182, 111, 209, 186, 160, 32, 142, 36, 35, 35, 246, 171, 100, 36, 210, 170, 3, 32, 235, 119, 216, 83, 132, 47, 194, 66, 220, 182, 179, 244, 150, 84, 249, 141, 114, 36, 5, 37, 162, 32, 38, 171, 88, 248, 104, 26, 131, 158, 151, 102, 130, 161 }, new byte[] { 120, 54, 33, 190, 131, 59, 144, 245, 159, 199, 250, 137, 160, 47, 56, 114, 141, 230, 192, 69, 141, 2, 173, 197, 236, 30, 157, 83, 146, 18, 52, 122, 175, 79, 130, 21, 244, 179, 110, 254, 68, 74, 214, 132, 18, 230, 12, 70, 228, 76, 224, 129, 179, 68, 250, 37, 171, 43, 34, 162, 200, 96, 231, 246, 29, 154, 164, 201, 118, 119, 142, 185, 33, 242, 223, 77, 145, 204, 116, 222, 134, 41, 39, 150, 103, 205, 166, 46, 151, 24, 93, 73, 216, 99, 215, 216, 88, 36, 8, 56, 101, 88, 18, 7, 148, 142, 177, 219, 168, 66, 42, 41, 82, 118, 6, 35, 211, 90, 212, 209, 129, 137, 95, 172, 40, 13, 144, 161 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 11, 85, 28, 91, 189, 249, 182, 113, 154, 180, 243, 130, 182, 111, 209, 186, 160, 32, 142, 36, 35, 35, 246, 171, 100, 36, 210, 170, 3, 32, 235, 119, 216, 83, 132, 47, 194, 66, 220, 182, 179, 244, 150, 84, 249, 141, 114, 36, 5, 37, 162, 32, 38, 171, 88, 248, 104, 26, 131, 158, 151, 102, 130, 161 }, new byte[] { 120, 54, 33, 190, 131, 59, 144, 245, 159, 199, 250, 137, 160, 47, 56, 114, 141, 230, 192, 69, 141, 2, 173, 197, 236, 30, 157, 83, 146, 18, 52, 122, 175, 79, 130, 21, 244, 179, 110, 254, 68, 74, 214, 132, 18, 230, 12, 70, 228, 76, 224, 129, 179, 68, 250, 37, 171, 43, 34, 162, 200, 96, 231, 246, 29, 154, 164, 201, 118, 119, 142, 185, 33, 242, 223, 77, 145, 204, 116, 222, 134, 41, 39, 150, 103, 205, 166, 46, 151, 24, 93, 73, 216, 99, 215, 216, 88, 36, 8, 56, 101, 88, 18, 7, 148, 142, 177, 219, 168, 66, 42, 41, 82, 118, 6, 35, 211, 90, 212, 209, 129, 137, 95, 172, 40, 13, 144, 161 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("3a7fe92f-b5be-4783-9e82-cdc1ea1571f7"), "Van", "KAY 215H" },
                    { new Guid("7b15c870-8f4c-471e-85d4-56b81a6aa657"), "Truck", "KDL 085M" },
                    { new Guid("80ecc1a4-305e-4ba8-99cb-3f0a9643aa00"), "Van", "KBF 462A" },
                    { new Guid("80faee9a-765e-4c1d-9ab7-4df3ebd6cfa2"), "Truck", "KCZ 595L" },
                    { new Guid("a20ab872-531b-4854-b040-e0b940d222f2"), "Truck", "KDB 387Q" },
                    { new Guid("da8aafef-8069-4120-a1f3-3e23f01cf139"), "Truck", "KCY 067A" },
                    { new Guid("ff1e5f8d-8deb-43e0-8204-9798afa4512d"), "Truck", "KDE 228S" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("0dfbb9c6-5499-4e54-940c-67ec9dbeb7be"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("4b2b392c-33db-4865-9be2-bd1b9df250e3"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("60ef3dab-bd2e-421d-9977-2c81e9470405"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("63756509-979b-494b-9e66-e96a4e58af17"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("6496dd47-95e6-497f-a48d-d213a3fbcb07"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ab94ee29-c601-4658-8fa7-d35e098c6eed"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ad37940d-3460-4f29-a78e-9cc302b2182b"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("aee08f71-fce3-45f4-9d2b-60b94d4d345c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("de86d3eb-707f-47e2-8ea9-7f75d3ce7126"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("eab8076f-5889-43fb-b7d7-bd76e8d8a42f"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("3a7fe92f-b5be-4783-9e82-cdc1ea1571f7"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("7b15c870-8f4c-471e-85d4-56b81a6aa657"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("80ecc1a4-305e-4ba8-99cb-3f0a9643aa00"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("80faee9a-765e-4c1d-9ab7-4df3ebd6cfa2"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("a20ab872-531b-4854-b040-e0b940d222f2"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("da8aafef-8069-4120-a1f3-3e23f01cf139"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff1e5f8d-8deb-43e0-8204-9798afa4512d"));

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "ParcelCreatedAt",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceItems",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("01e46104-5571-4357-adf2-26805ff5d003"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("2799bf53-ebd5-439f-a65a-6bd10adcf156"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("614cea0a-b928-4e9d-be69-996807d268f9"), "James", "JG", "Gichohi", "DL123456" },
                    { new Guid("6ead1e5a-9181-41e3-9085-261a6190bb6a"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("6f631350-e3ea-490b-8661-fb0440e22e09"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("813c59b8-3ae2-4ae6-8858-07e05b4e68e8"), "Charles", "CM", "Maina", "DL123456" },
                    { new Guid("8838002d-f7fe-4d6c-9dd5-3264de7caf75"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("b7b87f9a-fed6-4096-bb3e-e0d4a369e5e6"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("d77ec151-bf52-4771-bbd9-d449d2628a42"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("f44c7939-20ea-4ce9-bbd9-0adb6fe59818"), "Erastus", "EK", "Kagwa", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 49, 48, 97, 26, 236, 42, 82, 22, 212, 123, 11, 102, 62, 128, 1, 170, 35, 220, 187, 110, 189, 93, 228, 101, 18, 52, 112, 203, 56, 216, 153, 248, 32, 199, 44, 195, 197, 216, 26, 204, 247, 135, 199, 152, 177, 11, 189, 231, 94, 81, 201, 35, 159, 233, 3, 213, 254, 242, 230, 100, 222, 108, 228 }, new byte[] { 153, 165, 243, 71, 200, 175, 126, 224, 34, 26, 1, 142, 36, 62, 21, 254, 69, 49, 201, 64, 82, 84, 25, 167, 45, 207, 231, 36, 227, 75, 142, 202, 103, 206, 32, 130, 181, 163, 148, 200, 169, 230, 227, 129, 117, 200, 153, 109, 38, 94, 80, 138, 211, 194, 235, 98, 41, 31, 231, 124, 220, 141, 14, 115, 105, 38, 167, 49, 6, 39, 215, 158, 123, 14, 113, 211, 32, 170, 195, 123, 54, 188, 8, 177, 252, 109, 40, 110, 102, 196, 17, 129, 170, 132, 146, 149, 160, 240, 6, 89, 45, 20, 225, 206, 81, 10, 233, 87, 156, 240, 204, 46, 136, 230, 215, 17, 73, 103, 196, 220, 93, 28, 214, 141, 49, 116, 19, 90 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 49, 48, 97, 26, 236, 42, 82, 22, 212, 123, 11, 102, 62, 128, 1, 170, 35, 220, 187, 110, 189, 93, 228, 101, 18, 52, 112, 203, 56, 216, 153, 248, 32, 199, 44, 195, 197, 216, 26, 204, 247, 135, 199, 152, 177, 11, 189, 231, 94, 81, 201, 35, 159, 233, 3, 213, 254, 242, 230, 100, 222, 108, 228 }, new byte[] { 153, 165, 243, 71, 200, 175, 126, 224, 34, 26, 1, 142, 36, 62, 21, 254, 69, 49, 201, 64, 82, 84, 25, 167, 45, 207, 231, 36, 227, 75, 142, 202, 103, 206, 32, 130, 181, 163, 148, 200, 169, 230, 227, 129, 117, 200, 153, 109, 38, 94, 80, 138, 211, 194, 235, 98, 41, 31, 231, 124, 220, 141, 14, 115, 105, 38, 167, 49, 6, 39, 215, 158, 123, 14, 113, 211, 32, 170, 195, 123, 54, 188, 8, 177, 252, 109, 40, 110, 102, 196, 17, 129, 170, 132, 146, 149, 160, 240, 6, 89, 45, 20, 225, 206, 81, 10, 233, 87, 156, 240, 204, 46, 136, 230, 215, 17, 73, 103, 196, 220, 93, 28, 214, 141, 49, 116, 19, 90 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 49, 48, 97, 26, 236, 42, 82, 22, 212, 123, 11, 102, 62, 128, 1, 170, 35, 220, 187, 110, 189, 93, 228, 101, 18, 52, 112, 203, 56, 216, 153, 248, 32, 199, 44, 195, 197, 216, 26, 204, 247, 135, 199, 152, 177, 11, 189, 231, 94, 81, 201, 35, 159, 233, 3, 213, 254, 242, 230, 100, 222, 108, 228 }, new byte[] { 153, 165, 243, 71, 200, 175, 126, 224, 34, 26, 1, 142, 36, 62, 21, 254, 69, 49, 201, 64, 82, 84, 25, 167, 45, 207, 231, 36, 227, 75, 142, 202, 103, 206, 32, 130, 181, 163, 148, 200, 169, 230, 227, 129, 117, 200, 153, 109, 38, 94, 80, 138, 211, 194, 235, 98, 41, 31, 231, 124, 220, 141, 14, 115, 105, 38, 167, 49, 6, 39, 215, 158, 123, 14, 113, 211, 32, 170, 195, 123, 54, 188, 8, 177, 252, 109, 40, 110, 102, 196, 17, 129, 170, 132, 146, 149, 160, 240, 6, 89, 45, 20, 225, 206, 81, 10, 233, 87, 156, 240, 204, 46, 136, 230, 215, 17, 73, 103, 196, 220, 93, 28, 214, 141, 49, 116, 19, 90 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 49, 48, 97, 26, 236, 42, 82, 22, 212, 123, 11, 102, 62, 128, 1, 170, 35, 220, 187, 110, 189, 93, 228, 101, 18, 52, 112, 203, 56, 216, 153, 248, 32, 199, 44, 195, 197, 216, 26, 204, 247, 135, 199, 152, 177, 11, 189, 231, 94, 81, 201, 35, 159, 233, 3, 213, 254, 242, 230, 100, 222, 108, 228 }, new byte[] { 153, 165, 243, 71, 200, 175, 126, 224, 34, 26, 1, 142, 36, 62, 21, 254, 69, 49, 201, 64, 82, 84, 25, 167, 45, 207, 231, 36, 227, 75, 142, 202, 103, 206, 32, 130, 181, 163, 148, 200, 169, 230, 227, 129, 117, 200, 153, 109, 38, 94, 80, 138, 211, 194, 235, 98, 41, 31, 231, 124, 220, 141, 14, 115, 105, 38, 167, 49, 6, 39, 215, 158, 123, 14, 113, 211, 32, 170, 195, 123, 54, 188, 8, 177, 252, 109, 40, 110, 102, 196, 17, 129, 170, 132, 146, 149, 160, 240, 6, 89, 45, 20, 225, 206, 81, 10, 233, 87, 156, 240, 204, 46, 136, 230, 215, 17, 73, 103, 196, 220, 93, 28, 214, 141, 49, 116, 19, 90 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 49, 48, 97, 26, 236, 42, 82, 22, 212, 123, 11, 102, 62, 128, 1, 170, 35, 220, 187, 110, 189, 93, 228, 101, 18, 52, 112, 203, 56, 216, 153, 248, 32, 199, 44, 195, 197, 216, 26, 204, 247, 135, 199, 152, 177, 11, 189, 231, 94, 81, 201, 35, 159, 233, 3, 213, 254, 242, 230, 100, 222, 108, 228 }, new byte[] { 153, 165, 243, 71, 200, 175, 126, 224, 34, 26, 1, 142, 36, 62, 21, 254, 69, 49, 201, 64, 82, 84, 25, 167, 45, 207, 231, 36, 227, 75, 142, 202, 103, 206, 32, 130, 181, 163, 148, 200, 169, 230, 227, 129, 117, 200, 153, 109, 38, 94, 80, 138, 211, 194, 235, 98, 41, 31, 231, 124, 220, 141, 14, 115, 105, 38, 167, 49, 6, 39, 215, 158, 123, 14, 113, 211, 32, 170, 195, 123, 54, 188, 8, 177, 252, 109, 40, 110, 102, 196, 17, 129, 170, 132, 146, 149, 160, 240, 6, 89, 45, 20, 225, 206, 81, 10, 233, 87, 156, 240, 204, 46, 136, 230, 215, 17, 73, 103, 196, 220, 93, 28, 214, 141, 49, 116, 19, 90 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 49, 48, 97, 26, 236, 42, 82, 22, 212, 123, 11, 102, 62, 128, 1, 170, 35, 220, 187, 110, 189, 93, 228, 101, 18, 52, 112, 203, 56, 216, 153, 248, 32, 199, 44, 195, 197, 216, 26, 204, 247, 135, 199, 152, 177, 11, 189, 231, 94, 81, 201, 35, 159, 233, 3, 213, 254, 242, 230, 100, 222, 108, 228 }, new byte[] { 153, 165, 243, 71, 200, 175, 126, 224, 34, 26, 1, 142, 36, 62, 21, 254, 69, 49, 201, 64, 82, 84, 25, 167, 45, 207, 231, 36, 227, 75, 142, 202, 103, 206, 32, 130, 181, 163, 148, 200, 169, 230, 227, 129, 117, 200, 153, 109, 38, 94, 80, 138, 211, 194, 235, 98, 41, 31, 231, 124, 220, 141, 14, 115, 105, 38, 167, 49, 6, 39, 215, 158, 123, 14, 113, 211, 32, 170, 195, 123, 54, 188, 8, 177, 252, 109, 40, 110, 102, 196, 17, 129, 170, 132, 146, 149, 160, 240, 6, 89, 45, 20, 225, 206, 81, 10, 233, 87, 156, 240, 204, 46, 136, 230, 215, 17, 73, 103, 196, 220, 93, 28, 214, 141, 49, 116, 19, 90 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("1808b1fa-7bf1-44ac-9976-ac4fdcc83fc8"), "Truck", "KCZ 595L" },
                    { new Guid("190e3a46-3719-43f2-8a6c-ca6642986634"), "Van", "KBF 462A" },
                    { new Guid("52ecddc8-2aa8-4816-8312-75707ff22832"), "Truck", "KDE 228S" },
                    { new Guid("7c350bbd-9781-419b-b581-f52990410cd3"), "Truck", "KDB 387Q" },
                    { new Guid("7dfc1d4f-724c-41a5-8fa4-60590c68c8b5"), "Truck", "KDL 085M" },
                    { new Guid("888c1d4c-ff95-4fe6-8aa9-7b43e866ea17"), "Van", "KAY 215H" },
                    { new Guid("e32a6d10-b5c4-4782-bcaf-f9414a6c1dec"), "Truck", "KCY 067A" }
                });
        }
    }
}

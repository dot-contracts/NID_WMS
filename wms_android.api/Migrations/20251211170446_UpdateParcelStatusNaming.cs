using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParcelStatusNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("095232fb-d3df-4604-888e-63160d083194"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("79c0695e-2faf-407e-be48-9bc1b72c8a62"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("7ee49add-8f7f-4c89-917e-23ab18495f0c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8233b66a-d0ca-4392-85fe-4de96bff5e06"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("85834e40-92a3-41e4-82fd-29fdd9ca0d13"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("87457561-de53-443a-8be5-0d99d2ab6383"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("923f64da-8416-4780-b0fe-03b90eae2b5c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("990dfca5-f619-46aa-ad73-c0e071e74711"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("a6e1837b-f42f-4134-b29d-d939d48edcf7"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ea569014-1c38-4494-aa2a-7d9c34b8c9ad"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("58c70873-85e4-4ab8-8720-6a7921a5c666"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("7571df6f-7d50-4042-bb72-c8884eca6628"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("9e5d2c5f-0c93-4853-a6ec-5a12bb431777"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b9798a67-f89e-4564-8992-97e2227009fb"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("cb3f3cb6-fa0d-4e1f-8cef-d7edb12938b5"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("f4627c1d-1c8b-46ae-8d0e-a71d485c03ad"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fd79e494-2623-4455-8c1f-d66965918a42"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("095232fb-d3df-4604-888e-63160d083194"), "Charles", "CM", "Maina", "DL123456" },
                    { new Guid("79c0695e-2faf-407e-be48-9bc1b72c8a62"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("7ee49add-8f7f-4c89-917e-23ab18495f0c"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("8233b66a-d0ca-4392-85fe-4de96bff5e06"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("85834e40-92a3-41e4-82fd-29fdd9ca0d13"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("87457561-de53-443a-8be5-0d99d2ab6383"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("923f64da-8416-4780-b0fe-03b90eae2b5c"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("990dfca5-f619-46aa-ad73-c0e071e74711"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("a6e1837b-f42f-4134-b29d-d939d48edcf7"), "James", "JG", "Gichohi", "DL123456" },
                    { new Guid("ea569014-1c38-4494-aa2a-7d9c34b8c9ad"), "Erastus", "EK", "Kagwa", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 139, 218, 15, 36, 189, 160, 137, 245, 82, 111, 20, 253, 66, 14, 62, 186, 150, 123, 135, 210, 143, 219, 251, 97, 242, 221, 1, 34, 90, 40, 87, 209, 186, 57, 156, 130, 15, 48, 239, 224, 223, 131, 236, 70, 219, 151, 125, 6, 27, 37, 28, 209, 88, 222, 139, 221, 190, 252, 113, 243, 252, 89, 84, 60 }, new byte[] { 199, 102, 10, 165, 140, 8, 154, 109, 104, 226, 36, 176, 59, 184, 6, 139, 223, 176, 86, 37, 170, 96, 138, 49, 84, 109, 240, 160, 232, 248, 161, 76, 187, 210, 178, 198, 253, 219, 72, 241, 71, 114, 171, 237, 48, 59, 49, 54, 233, 214, 110, 113, 220, 161, 100, 182, 136, 54, 36, 14, 236, 231, 139, 169, 164, 248, 97, 239, 69, 72, 121, 141, 54, 37, 127, 47, 200, 133, 183, 48, 237, 110, 30, 44, 203, 108, 120, 121, 60, 195, 7, 66, 71, 63, 160, 204, 155, 82, 99, 252, 162, 150, 70, 113, 188, 248, 128, 53, 159, 73, 201, 235, 13, 72, 74, 119, 54, 41, 167, 98, 41, 97, 88, 219, 93, 23, 35, 234 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("58c70873-85e4-4ab8-8720-6a7921a5c666"), "Truck", "KDE 228S" },
                    { new Guid("7571df6f-7d50-4042-bb72-c8884eca6628"), "Truck", "KCY 067A" },
                    { new Guid("9e5d2c5f-0c93-4853-a6ec-5a12bb431777"), "Van", "KBF 462A" },
                    { new Guid("b9798a67-f89e-4564-8992-97e2227009fb"), "Van", "KAY 215H" },
                    { new Guid("cb3f3cb6-fa0d-4e1f-8cef-d7edb12938b5"), "Truck", "KCZ 595L" },
                    { new Guid("f4627c1d-1c8b-46ae-8d0e-a71d485c03ad"), "Truck", "KDL 085M" },
                    { new Guid("fd79e494-2623-4455-8c1f-d66965918a42"), "Truck", "KDB 387Q" }
                });
        }
    }
}

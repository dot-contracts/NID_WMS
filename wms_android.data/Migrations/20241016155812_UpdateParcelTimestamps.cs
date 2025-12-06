using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParcelTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("14751c62-c52c-45f9-b1ef-f5c513ff4b85"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("333a4593-7774-4fa9-aafe-c0f30a5b57e1"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("548da581-0665-44bd-a120-88a399ff0b33"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("bedab3e5-f48d-4177-be3d-7e2aab4a32b7"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("bf52aa29-6ffa-4ddc-8c45-c92902864827"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c6c22e93-989e-4e2b-8f6e-6c44b59e1fe2"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("cc116c92-54fb-4464-8051-349746cf1425"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d2ace635-4885-4447-b298-fa8a61f8206b"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d35cbb60-b1c4-4ba7-b509-ae4b6f2d9342"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("d5a4d9fa-38d6-4623-b51c-a02d36354c34"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("0f3618a4-62e8-4dfe-b6aa-fce3ad1c80b9"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("33a827c7-b955-4f16-933e-f274d8d0ca34"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("480395ba-ecf2-46cd-b4ef-e1b47610d775"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b22cc81b-ec0c-4cda-b08e-f6fa9844b1f4"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b9476fdf-3af6-4322-8154-7532f8c63a4a"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c285938b-5731-4ae5-9f4a-f2debbd40e5f"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d8141bdf-239d-4f3c-8ee8-f6993bd14e78"));

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("052e6db7-f980-4c64-9b2b-bdefabf8a9ec"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("07311cdc-2cf0-42f3-ba76-7cdd8413e3af"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("105c578f-600e-4a74-9fbd-7283744d0e89"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("3b8babf8-e509-4ebc-9a9a-7dec49bfbcff"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("63b78ec9-bff0-4749-a1bc-42f0ce7aa84c"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("7deb66c6-d1a1-4375-8cc4-c223dc7b0dff"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("ab1ee34b-ad82-4d5a-8127-ab50f8f1c616"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("bc8144dd-c544-4b62-8a1f-da8948091017"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("c8233bdf-e9a2-41e0-a8ec-1b294de63806"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("fe53e419-32a5-4c6a-956a-20b3b4d313ab"), "Charles", "SK", "Maina", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 237, 163, 41, 115, 114, 170, 224, 236, 233, 171, 241, 29, 2, 90, 93, 208, 80, 101, 130, 46, 4, 147, 120, 146, 46, 14, 193, 28, 195, 54, 170, 60, 162, 7, 128, 184, 49, 248, 77, 135, 255, 16, 189, 211, 59, 227, 58, 173, 12, 32, 211, 1, 197, 160, 177, 96, 105, 238, 205, 108, 196, 98, 40 }, new byte[] { 92, 113, 74, 74, 107, 163, 142, 158, 22, 88, 51, 20, 60, 207, 205, 209, 21, 77, 211, 249, 173, 231, 78, 100, 220, 177, 171, 53, 248, 25, 94, 154, 67, 235, 91, 236, 152, 251, 189, 73, 197, 98, 118, 67, 56, 209, 114, 158, 84, 171, 14, 152, 80, 24, 175, 15, 242, 126, 224, 161, 123, 0, 167, 159, 51, 224, 134, 128, 232, 21, 254, 133, 105, 68, 28, 172, 95, 84, 243, 200, 81, 11, 230, 118, 7, 172, 22, 249, 64, 197, 204, 182, 21, 63, 222, 170, 10, 114, 117, 206, 232, 90, 5, 77, 176, 146, 123, 219, 21, 73, 64, 32, 82, 227, 20, 96, 146, 5, 145, 236, 45, 18, 104, 213, 242, 134, 71, 216 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 237, 163, 41, 115, 114, 170, 224, 236, 233, 171, 241, 29, 2, 90, 93, 208, 80, 101, 130, 46, 4, 147, 120, 146, 46, 14, 193, 28, 195, 54, 170, 60, 162, 7, 128, 184, 49, 248, 77, 135, 255, 16, 189, 211, 59, 227, 58, 173, 12, 32, 211, 1, 197, 160, 177, 96, 105, 238, 205, 108, 196, 98, 40 }, new byte[] { 92, 113, 74, 74, 107, 163, 142, 158, 22, 88, 51, 20, 60, 207, 205, 209, 21, 77, 211, 249, 173, 231, 78, 100, 220, 177, 171, 53, 248, 25, 94, 154, 67, 235, 91, 236, 152, 251, 189, 73, 197, 98, 118, 67, 56, 209, 114, 158, 84, 171, 14, 152, 80, 24, 175, 15, 242, 126, 224, 161, 123, 0, 167, 159, 51, 224, 134, 128, 232, 21, 254, 133, 105, 68, 28, 172, 95, 84, 243, 200, 81, 11, 230, 118, 7, 172, 22, 249, 64, 197, 204, 182, 21, 63, 222, 170, 10, 114, 117, 206, 232, 90, 5, 77, 176, 146, 123, 219, 21, 73, 64, 32, 82, 227, 20, 96, 146, 5, 145, 236, 45, 18, 104, 213, 242, 134, 71, 216 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 237, 163, 41, 115, 114, 170, 224, 236, 233, 171, 241, 29, 2, 90, 93, 208, 80, 101, 130, 46, 4, 147, 120, 146, 46, 14, 193, 28, 195, 54, 170, 60, 162, 7, 128, 184, 49, 248, 77, 135, 255, 16, 189, 211, 59, 227, 58, 173, 12, 32, 211, 1, 197, 160, 177, 96, 105, 238, 205, 108, 196, 98, 40 }, new byte[] { 92, 113, 74, 74, 107, 163, 142, 158, 22, 88, 51, 20, 60, 207, 205, 209, 21, 77, 211, 249, 173, 231, 78, 100, 220, 177, 171, 53, 248, 25, 94, 154, 67, 235, 91, 236, 152, 251, 189, 73, 197, 98, 118, 67, 56, 209, 114, 158, 84, 171, 14, 152, 80, 24, 175, 15, 242, 126, 224, 161, 123, 0, 167, 159, 51, 224, 134, 128, 232, 21, 254, 133, 105, 68, 28, 172, 95, 84, 243, 200, 81, 11, 230, 118, 7, 172, 22, 249, 64, 197, 204, 182, 21, 63, 222, 170, 10, 114, 117, 206, 232, 90, 5, 77, 176, 146, 123, 219, 21, 73, 64, 32, 82, 227, 20, 96, 146, 5, 145, 236, 45, 18, 104, 213, 242, 134, 71, 216 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 237, 163, 41, 115, 114, 170, 224, 236, 233, 171, 241, 29, 2, 90, 93, 208, 80, 101, 130, 46, 4, 147, 120, 146, 46, 14, 193, 28, 195, 54, 170, 60, 162, 7, 128, 184, 49, 248, 77, 135, 255, 16, 189, 211, 59, 227, 58, 173, 12, 32, 211, 1, 197, 160, 177, 96, 105, 238, 205, 108, 196, 98, 40 }, new byte[] { 92, 113, 74, 74, 107, 163, 142, 158, 22, 88, 51, 20, 60, 207, 205, 209, 21, 77, 211, 249, 173, 231, 78, 100, 220, 177, 171, 53, 248, 25, 94, 154, 67, 235, 91, 236, 152, 251, 189, 73, 197, 98, 118, 67, 56, 209, 114, 158, 84, 171, 14, 152, 80, 24, 175, 15, 242, 126, 224, 161, 123, 0, 167, 159, 51, 224, 134, 128, 232, 21, 254, 133, 105, 68, 28, 172, 95, 84, 243, 200, 81, 11, 230, 118, 7, 172, 22, 249, 64, 197, 204, 182, 21, 63, 222, 170, 10, 114, 117, 206, 232, 90, 5, 77, 176, 146, 123, 219, 21, 73, 64, 32, 82, 227, 20, 96, 146, 5, 145, 236, 45, 18, 104, 213, 242, 134, 71, 216 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 176, 237, 163, 41, 115, 114, 170, 224, 236, 233, 171, 241, 29, 2, 90, 93, 208, 80, 101, 130, 46, 4, 147, 120, 146, 46, 14, 193, 28, 195, 54, 170, 60, 162, 7, 128, 184, 49, 248, 77, 135, 255, 16, 189, 211, 59, 227, 58, 173, 12, 32, 211, 1, 197, 160, 177, 96, 105, 238, 205, 108, 196, 98, 40 }, new byte[] { 92, 113, 74, 74, 107, 163, 142, 158, 22, 88, 51, 20, 60, 207, 205, 209, 21, 77, 211, 249, 173, 231, 78, 100, 220, 177, 171, 53, 248, 25, 94, 154, 67, 235, 91, 236, 152, 251, 189, 73, 197, 98, 118, 67, 56, 209, 114, 158, 84, 171, 14, 152, 80, 24, 175, 15, 242, 126, 224, 161, 123, 0, 167, 159, 51, 224, 134, 128, 232, 21, 254, 133, 105, 68, 28, 172, 95, 84, 243, 200, 81, 11, 230, 118, 7, 172, 22, 249, 64, 197, 204, 182, 21, 63, 222, 170, 10, 114, 117, 206, 232, 90, 5, 77, 176, 146, 123, 219, 21, 73, 64, 32, 82, 227, 20, 96, 146, 5, 145, 236, 45, 18, 104, 213, 242, 134, 71, 216 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("1d5fd780-4bc4-40ca-b1ed-3338db4fece2"), "Truck", "KDB 387Q" },
                    { new Guid("56a0a490-ddc6-44c9-a5a9-c3a7d95223a7"), "Truck", "KDE 228S" },
                    { new Guid("6a80a127-61f0-4a4a-b52e-bb0bb3752447"), "Van", "KBF 462A" },
                    { new Guid("9283a6dc-6b1e-40bb-badc-7c1b67201d4e"), "Truck", "KCZ 595L" },
                    { new Guid("9c2ba133-bc28-4bfe-81b6-5880a493e742"), "Truck", "KDL 085M" },
                    { new Guid("a4f281dd-c9f4-400e-8244-8e4604c347d5"), "Truck", "KCY 067A" },
                    { new Guid("d5011e71-bbcf-463b-b797-c3092fb51816"), "Van", "KAY 215H" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("052e6db7-f980-4c64-9b2b-bdefabf8a9ec"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("07311cdc-2cf0-42f3-ba76-7cdd8413e3af"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("105c578f-600e-4a74-9fbd-7283744d0e89"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("3b8babf8-e509-4ebc-9a9a-7dec49bfbcff"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("63b78ec9-bff0-4749-a1bc-42f0ce7aa84c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("7deb66c6-d1a1-4375-8cc4-c223dc7b0dff"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ab1ee34b-ad82-4d5a-8127-ab50f8f1c616"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("bc8144dd-c544-4b62-8a1f-da8948091017"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c8233bdf-e9a2-41e0-a8ec-1b294de63806"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("fe53e419-32a5-4c6a-956a-20b3b4d313ab"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("1d5fd780-4bc4-40ca-b1ed-3338db4fece2"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("56a0a490-ddc6-44c9-a5a9-c3a7d95223a7"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a80a127-61f0-4a4a-b52e-bb0bb3752447"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("9283a6dc-6b1e-40bb-badc-7c1b67201d4e"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("9c2ba133-bc28-4bfe-81b6-5880a493e742"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("a4f281dd-c9f4-400e-8244-8e4604c347d5"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d5011e71-bbcf-463b-b797-c3092fb51816"));

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("14751c62-c52c-45f9-b1ef-f5c513ff4b85"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("333a4593-7774-4fa9-aafe-c0f30a5b57e1"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("548da581-0665-44bd-a120-88a399ff0b33"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("bedab3e5-f48d-4177-be3d-7e2aab4a32b7"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("bf52aa29-6ffa-4ddc-8c45-c92902864827"), "Charles", "SK", "Maina", "DL123456" },
                    { new Guid("c6c22e93-989e-4e2b-8f6e-6c44b59e1fe2"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("cc116c92-54fb-4464-8051-349746cf1425"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("d2ace635-4885-4447-b298-fa8a61f8206b"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("d35cbb60-b1c4-4ba7-b509-ae4b6f2d9342"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("d5a4d9fa-38d6-4623-b51c-a02d36354c34"), "James", "SK", "Gichohi", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 146, 141, 40, 139, 249, 144, 101, 163, 243, 10, 126, 101, 151, 97, 208, 45, 89, 240, 157, 121, 97, 28, 90, 254, 188, 15, 189, 48, 171, 56, 137, 16, 194, 161, 157, 189, 122, 2, 214, 197, 173, 179, 189, 106, 140, 22, 2, 8, 39, 25, 242, 210, 135, 97, 194, 21, 236, 117, 45, 30, 147, 43, 10, 168 }, new byte[] { 32, 47, 188, 24, 90, 108, 36, 142, 168, 74, 144, 219, 76, 187, 50, 161, 188, 120, 205, 164, 75, 178, 132, 120, 10, 123, 89, 36, 182, 206, 62, 209, 179, 233, 219, 195, 238, 102, 167, 176, 29, 19, 24, 48, 66, 58, 43, 6, 5, 151, 115, 95, 129, 80, 6, 74, 0, 217, 114, 36, 215, 218, 182, 242, 124, 246, 82, 66, 206, 115, 135, 150, 38, 154, 169, 213, 170, 37, 63, 213, 228, 177, 146, 231, 24, 58, 181, 36, 2, 208, 118, 143, 163, 129, 9, 145, 203, 166, 24, 159, 17, 187, 128, 240, 125, 53, 26, 254, 8, 24, 98, 182, 98, 225, 251, 246, 105, 204, 224, 124, 220, 55, 191, 138, 86, 95, 153, 130 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 146, 141, 40, 139, 249, 144, 101, 163, 243, 10, 126, 101, 151, 97, 208, 45, 89, 240, 157, 121, 97, 28, 90, 254, 188, 15, 189, 48, 171, 56, 137, 16, 194, 161, 157, 189, 122, 2, 214, 197, 173, 179, 189, 106, 140, 22, 2, 8, 39, 25, 242, 210, 135, 97, 194, 21, 236, 117, 45, 30, 147, 43, 10, 168 }, new byte[] { 32, 47, 188, 24, 90, 108, 36, 142, 168, 74, 144, 219, 76, 187, 50, 161, 188, 120, 205, 164, 75, 178, 132, 120, 10, 123, 89, 36, 182, 206, 62, 209, 179, 233, 219, 195, 238, 102, 167, 176, 29, 19, 24, 48, 66, 58, 43, 6, 5, 151, 115, 95, 129, 80, 6, 74, 0, 217, 114, 36, 215, 218, 182, 242, 124, 246, 82, 66, 206, 115, 135, 150, 38, 154, 169, 213, 170, 37, 63, 213, 228, 177, 146, 231, 24, 58, 181, 36, 2, 208, 118, 143, 163, 129, 9, 145, 203, 166, 24, 159, 17, 187, 128, 240, 125, 53, 26, 254, 8, 24, 98, 182, 98, 225, 251, 246, 105, 204, 224, 124, 220, 55, 191, 138, 86, 95, 153, 130 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 146, 141, 40, 139, 249, 144, 101, 163, 243, 10, 126, 101, 151, 97, 208, 45, 89, 240, 157, 121, 97, 28, 90, 254, 188, 15, 189, 48, 171, 56, 137, 16, 194, 161, 157, 189, 122, 2, 214, 197, 173, 179, 189, 106, 140, 22, 2, 8, 39, 25, 242, 210, 135, 97, 194, 21, 236, 117, 45, 30, 147, 43, 10, 168 }, new byte[] { 32, 47, 188, 24, 90, 108, 36, 142, 168, 74, 144, 219, 76, 187, 50, 161, 188, 120, 205, 164, 75, 178, 132, 120, 10, 123, 89, 36, 182, 206, 62, 209, 179, 233, 219, 195, 238, 102, 167, 176, 29, 19, 24, 48, 66, 58, 43, 6, 5, 151, 115, 95, 129, 80, 6, 74, 0, 217, 114, 36, 215, 218, 182, 242, 124, 246, 82, 66, 206, 115, 135, 150, 38, 154, 169, 213, 170, 37, 63, 213, 228, 177, 146, 231, 24, 58, 181, 36, 2, 208, 118, 143, 163, 129, 9, 145, 203, 166, 24, 159, 17, 187, 128, 240, 125, 53, 26, 254, 8, 24, 98, 182, 98, 225, 251, 246, 105, 204, 224, 124, 220, 55, 191, 138, 86, 95, 153, 130 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 146, 141, 40, 139, 249, 144, 101, 163, 243, 10, 126, 101, 151, 97, 208, 45, 89, 240, 157, 121, 97, 28, 90, 254, 188, 15, 189, 48, 171, 56, 137, 16, 194, 161, 157, 189, 122, 2, 214, 197, 173, 179, 189, 106, 140, 22, 2, 8, 39, 25, 242, 210, 135, 97, 194, 21, 236, 117, 45, 30, 147, 43, 10, 168 }, new byte[] { 32, 47, 188, 24, 90, 108, 36, 142, 168, 74, 144, 219, 76, 187, 50, 161, 188, 120, 205, 164, 75, 178, 132, 120, 10, 123, 89, 36, 182, 206, 62, 209, 179, 233, 219, 195, 238, 102, 167, 176, 29, 19, 24, 48, 66, 58, 43, 6, 5, 151, 115, 95, 129, 80, 6, 74, 0, 217, 114, 36, 215, 218, 182, 242, 124, 246, 82, 66, 206, 115, 135, 150, 38, 154, 169, 213, 170, 37, 63, 213, 228, 177, 146, 231, 24, 58, 181, 36, 2, 208, 118, 143, 163, 129, 9, 145, 203, 166, 24, 159, 17, 187, 128, 240, 125, 53, 26, 254, 8, 24, 98, 182, 98, 225, 251, 246, 105, 204, 224, 124, 220, 55, 191, 138, 86, 95, 153, 130 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 146, 141, 40, 139, 249, 144, 101, 163, 243, 10, 126, 101, 151, 97, 208, 45, 89, 240, 157, 121, 97, 28, 90, 254, 188, 15, 189, 48, 171, 56, 137, 16, 194, 161, 157, 189, 122, 2, 214, 197, 173, 179, 189, 106, 140, 22, 2, 8, 39, 25, 242, 210, 135, 97, 194, 21, 236, 117, 45, 30, 147, 43, 10, 168 }, new byte[] { 32, 47, 188, 24, 90, 108, 36, 142, 168, 74, 144, 219, 76, 187, 50, 161, 188, 120, 205, 164, 75, 178, 132, 120, 10, 123, 89, 36, 182, 206, 62, 209, 179, 233, 219, 195, 238, 102, 167, 176, 29, 19, 24, 48, 66, 58, 43, 6, 5, 151, 115, 95, 129, 80, 6, 74, 0, 217, 114, 36, 215, 218, 182, 242, 124, 246, 82, 66, 206, 115, 135, 150, 38, 154, 169, 213, 170, 37, 63, 213, 228, 177, 146, 231, 24, 58, 181, 36, 2, 208, 118, 143, 163, 129, 9, 145, 203, 166, 24, 159, 17, 187, 128, 240, 125, 53, 26, 254, 8, 24, 98, 182, 98, 225, 251, 246, 105, 204, 224, 124, 220, 55, 191, 138, 86, 95, 153, 130 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("0f3618a4-62e8-4dfe-b6aa-fce3ad1c80b9"), "Truck", "KCY 067A" },
                    { new Guid("33a827c7-b955-4f16-933e-f274d8d0ca34"), "Truck", "KCZ 595L" },
                    { new Guid("480395ba-ecf2-46cd-b4ef-e1b47610d775"), "Van", "KBF 462A" },
                    { new Guid("b22cc81b-ec0c-4cda-b08e-f6fa9844b1f4"), "Truck", "KDL 085M" },
                    { new Guid("b9476fdf-3af6-4322-8154-7532f8c63a4a"), "Truck", "KDB 387Q" },
                    { new Guid("c285938b-5731-4ae5-9f4a-f2debbd40e5f"), "Truck", "KDE 228S" },
                    { new Guid("d8141bdf-239d-4f3c-8ee8-f6993bd14e78"), "Van", "KAY 215H" }
                });
        }
    }
}

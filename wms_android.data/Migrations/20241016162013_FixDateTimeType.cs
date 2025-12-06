using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class FixDateTimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("11ceabc4-4d8d-4fbd-85d0-5abe53881797"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("1cabcf11-bfa9-4e32-9db5-10630fc66568"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("1cc71fff-97b3-4b72-8c4f-bcf66fb82880"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("289d13f6-b87c-475c-b8d2-49735ddcddfd"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("3a3c514d-767f-4ef7-b6ba-2adacdbd318a"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("504ae303-b8cf-4416-84a7-e51736d45a72"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("8db8838e-49bf-4c52-b350-c641d8cebc94"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("9ca13f45-a62a-49cd-b4c5-d75e59896213"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("c00e9133-8a7f-4174-9d7a-5d8957ca8e33"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("c6cbc754-c6b5-4ecd-919d-b1840502df91"), "Charles", "SK", "Maina", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 16, 16, 102, 101, 105, 40, 158, 175, 226, 84, 198, 37, 233, 104, 167, 3, 110, 253, 139, 252, 140, 44, 104, 35, 36, 243, 91, 51, 105, 45, 142, 73, 49, 158, 154, 181, 64, 99, 227, 26, 216, 79, 80, 240, 209, 74, 0, 71, 182, 128, 184, 23, 207, 136, 105, 47, 50, 122, 67, 78, 39, 124, 178, 19 }, new byte[] { 10, 7, 102, 234, 22, 210, 29, 156, 82, 95, 102, 89, 180, 28, 190, 48, 66, 24, 9, 25, 98, 219, 138, 255, 44, 154, 60, 218, 228, 117, 157, 186, 194, 63, 169, 168, 225, 90, 135, 196, 95, 116, 58, 22, 110, 145, 180, 135, 207, 184, 46, 6, 47, 23, 186, 149, 123, 33, 14, 97, 193, 176, 5, 6, 171, 3, 174, 118, 239, 130, 147, 111, 212, 34, 139, 180, 66, 123, 181, 2, 80, 192, 152, 237, 139, 139, 139, 79, 147, 117, 214, 5, 57, 235, 69, 241, 125, 36, 65, 226, 29, 147, 124, 26, 43, 235, 190, 230, 4, 169, 7, 161, 143, 150, 208, 227, 231, 90, 136, 48, 95, 81, 139, 235, 22, 52, 12, 137 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 16, 16, 102, 101, 105, 40, 158, 175, 226, 84, 198, 37, 233, 104, 167, 3, 110, 253, 139, 252, 140, 44, 104, 35, 36, 243, 91, 51, 105, 45, 142, 73, 49, 158, 154, 181, 64, 99, 227, 26, 216, 79, 80, 240, 209, 74, 0, 71, 182, 128, 184, 23, 207, 136, 105, 47, 50, 122, 67, 78, 39, 124, 178, 19 }, new byte[] { 10, 7, 102, 234, 22, 210, 29, 156, 82, 95, 102, 89, 180, 28, 190, 48, 66, 24, 9, 25, 98, 219, 138, 255, 44, 154, 60, 218, 228, 117, 157, 186, 194, 63, 169, 168, 225, 90, 135, 196, 95, 116, 58, 22, 110, 145, 180, 135, 207, 184, 46, 6, 47, 23, 186, 149, 123, 33, 14, 97, 193, 176, 5, 6, 171, 3, 174, 118, 239, 130, 147, 111, 212, 34, 139, 180, 66, 123, 181, 2, 80, 192, 152, 237, 139, 139, 139, 79, 147, 117, 214, 5, 57, 235, 69, 241, 125, 36, 65, 226, 29, 147, 124, 26, 43, 235, 190, 230, 4, 169, 7, 161, 143, 150, 208, 227, 231, 90, 136, 48, 95, 81, 139, 235, 22, 52, 12, 137 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 16, 16, 102, 101, 105, 40, 158, 175, 226, 84, 198, 37, 233, 104, 167, 3, 110, 253, 139, 252, 140, 44, 104, 35, 36, 243, 91, 51, 105, 45, 142, 73, 49, 158, 154, 181, 64, 99, 227, 26, 216, 79, 80, 240, 209, 74, 0, 71, 182, 128, 184, 23, 207, 136, 105, 47, 50, 122, 67, 78, 39, 124, 178, 19 }, new byte[] { 10, 7, 102, 234, 22, 210, 29, 156, 82, 95, 102, 89, 180, 28, 190, 48, 66, 24, 9, 25, 98, 219, 138, 255, 44, 154, 60, 218, 228, 117, 157, 186, 194, 63, 169, 168, 225, 90, 135, 196, 95, 116, 58, 22, 110, 145, 180, 135, 207, 184, 46, 6, 47, 23, 186, 149, 123, 33, 14, 97, 193, 176, 5, 6, 171, 3, 174, 118, 239, 130, 147, 111, 212, 34, 139, 180, 66, 123, 181, 2, 80, 192, 152, 237, 139, 139, 139, 79, 147, 117, 214, 5, 57, 235, 69, 241, 125, 36, 65, 226, 29, 147, 124, 26, 43, 235, 190, 230, 4, 169, 7, 161, 143, 150, 208, 227, 231, 90, 136, 48, 95, 81, 139, 235, 22, 52, 12, 137 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 16, 16, 102, 101, 105, 40, 158, 175, 226, 84, 198, 37, 233, 104, 167, 3, 110, 253, 139, 252, 140, 44, 104, 35, 36, 243, 91, 51, 105, 45, 142, 73, 49, 158, 154, 181, 64, 99, 227, 26, 216, 79, 80, 240, 209, 74, 0, 71, 182, 128, 184, 23, 207, 136, 105, 47, 50, 122, 67, 78, 39, 124, 178, 19 }, new byte[] { 10, 7, 102, 234, 22, 210, 29, 156, 82, 95, 102, 89, 180, 28, 190, 48, 66, 24, 9, 25, 98, 219, 138, 255, 44, 154, 60, 218, 228, 117, 157, 186, 194, 63, 169, 168, 225, 90, 135, 196, 95, 116, 58, 22, 110, 145, 180, 135, 207, 184, 46, 6, 47, 23, 186, 149, 123, 33, 14, 97, 193, 176, 5, 6, 171, 3, 174, 118, 239, 130, 147, 111, 212, 34, 139, 180, 66, 123, 181, 2, 80, 192, 152, 237, 139, 139, 139, 79, 147, 117, 214, 5, 57, 235, 69, 241, 125, 36, 65, 226, 29, 147, 124, 26, 43, 235, 190, 230, 4, 169, 7, 161, 143, 150, 208, 227, 231, 90, 136, 48, 95, 81, 139, 235, 22, 52, 12, 137 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 16, 16, 102, 101, 105, 40, 158, 175, 226, 84, 198, 37, 233, 104, 167, 3, 110, 253, 139, 252, 140, 44, 104, 35, 36, 243, 91, 51, 105, 45, 142, 73, 49, 158, 154, 181, 64, 99, 227, 26, 216, 79, 80, 240, 209, 74, 0, 71, 182, 128, 184, 23, 207, 136, 105, 47, 50, 122, 67, 78, 39, 124, 178, 19 }, new byte[] { 10, 7, 102, 234, 22, 210, 29, 156, 82, 95, 102, 89, 180, 28, 190, 48, 66, 24, 9, 25, 98, 219, 138, 255, 44, 154, 60, 218, 228, 117, 157, 186, 194, 63, 169, 168, 225, 90, 135, 196, 95, 116, 58, 22, 110, 145, 180, 135, 207, 184, 46, 6, 47, 23, 186, 149, 123, 33, 14, 97, 193, 176, 5, 6, 171, 3, 174, 118, 239, 130, 147, 111, 212, 34, 139, 180, 66, 123, 181, 2, 80, 192, 152, 237, 139, 139, 139, 79, 147, 117, 214, 5, 57, 235, 69, 241, 125, 36, 65, 226, 29, 147, 124, 26, 43, 235, 190, 230, 4, 169, 7, 161, 143, 150, 208, 227, 231, 90, 136, 48, 95, 81, 139, 235, 22, 52, 12, 137 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("11b5e0dd-8699-4432-b216-bd2799408716"), "Truck", "KCZ 595L" },
                    { new Guid("526365ca-b37a-4b43-b1b8-876b2055e028"), "Truck", "KCY 067A" },
                    { new Guid("79b2a1b1-29fd-444c-8540-fbba94f8e330"), "Truck", "KDL 085M" },
                    { new Guid("826b498f-94af-423f-afcb-f5d5edc146ec"), "Van", "KBF 462A" },
                    { new Guid("8dbb49a8-4f67-4480-8339-451de202fe32"), "Van", "KAY 215H" },
                    { new Guid("d17f4e37-01ed-4956-80fe-e50121a15e9e"), "Truck", "KDB 387Q" },
                    { new Guid("f4ab06a0-a204-4d3b-b15e-ed01418e7943"), "Truck", "KDE 228S" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("11ceabc4-4d8d-4fbd-85d0-5abe53881797"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("1cabcf11-bfa9-4e32-9db5-10630fc66568"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("1cc71fff-97b3-4b72-8c4f-bcf66fb82880"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("289d13f6-b87c-475c-b8d2-49735ddcddfd"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("3a3c514d-767f-4ef7-b6ba-2adacdbd318a"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("504ae303-b8cf-4416-84a7-e51736d45a72"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8db8838e-49bf-4c52-b350-c641d8cebc94"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("9ca13f45-a62a-49cd-b4c5-d75e59896213"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c00e9133-8a7f-4174-9d7a-5d8957ca8e33"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c6cbc754-c6b5-4ecd-919d-b1840502df91"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("11b5e0dd-8699-4432-b216-bd2799408716"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("526365ca-b37a-4b43-b1b8-876b2055e028"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("79b2a1b1-29fd-444c-8540-fbba94f8e330"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("826b498f-94af-423f-afcb-f5d5edc146ec"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("8dbb49a8-4f67-4480-8339-451de202fe32"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d17f4e37-01ed-4956-80fe-e50121a15e9e"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("f4ab06a0-a204-4d3b-b15e-ed01418e7943"));

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
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Parcels",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispatchedAt",
                table: "Parcels",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSeedDataWithProperHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispatchedAt",
                table: "Parcels",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Parcels",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("2c1acc53-f918-4d52-be58-f03fa4b385ac"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("4345ae39-6c57-46a5-9379-79226e2e0884"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("4944f6dd-f65e-4cbf-ab80-6e15536f8523"), "Robert", "RN", "Njuguna", "DL123456" },
                    { new Guid("4fa2c80e-b1a8-4793-b7d1-0c906becac48"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("563e12d1-808b-4aed-9109-c6a99472c17b"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("8973ab59-ecf9-4765-a50e-b976745d001c"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("8b2f5372-2dfa-481a-a512-5d17553517ac"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("af1b8bc2-d29f-4a5b-97c2-b4f0e92afa66"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("ca98ce76-0923-4a30-b67a-5dd5fd5d6b15"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("f7df347d-07bd-4a26-bd5b-1e2b7ec43a60"), "Charles", "SK", "Maina", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 140, 200, 27, 49, 174, 104, 10, 127, 96, 124, 245, 57, 251, 255, 197, 11, 25, 240, 85, 105, 226, 236, 188, 64, 1, 4, 23, 64, 126, 94, 92, 161, 216, 64, 84, 194, 20, 107, 18, 90, 203, 159, 26, 30, 157, 126, 80, 136, 92, 116, 81, 37, 6, 82, 154, 80, 73, 101, 134, 241, 162, 2, 16 }, new byte[] { 145, 192, 223, 184, 57, 115, 216, 185, 98, 75, 12, 193, 55, 255, 215, 8, 108, 86, 90, 217, 120, 174, 189, 232, 243, 14, 108, 164, 230, 91, 202, 44, 157, 207, 222, 28, 132, 255, 70, 191, 241, 147, 128, 88, 182, 195, 71, 186, 103, 189, 220, 114, 215, 205, 199, 3, 45, 199, 117, 177, 115, 219, 186, 180, 69, 217, 8, 161, 106, 236, 69, 154, 151, 138, 2, 220, 85, 153, 181, 119, 36, 75, 184, 132, 73, 50, 127, 23, 219, 161, 109, 33, 43, 29, 208, 26, 12, 23, 214, 135, 20, 88, 236, 180, 14, 238, 150, 7, 16, 88, 125, 241, 172, 84, 139, 20, 45, 129, 189, 217, 66, 242, 162, 32, 20, 24, 151, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 140, 200, 27, 49, 174, 104, 10, 127, 96, 124, 245, 57, 251, 255, 197, 11, 25, 240, 85, 105, 226, 236, 188, 64, 1, 4, 23, 64, 126, 94, 92, 161, 216, 64, 84, 194, 20, 107, 18, 90, 203, 159, 26, 30, 157, 126, 80, 136, 92, 116, 81, 37, 6, 82, 154, 80, 73, 101, 134, 241, 162, 2, 16 }, new byte[] { 145, 192, 223, 184, 57, 115, 216, 185, 98, 75, 12, 193, 55, 255, 215, 8, 108, 86, 90, 217, 120, 174, 189, 232, 243, 14, 108, 164, 230, 91, 202, 44, 157, 207, 222, 28, 132, 255, 70, 191, 241, 147, 128, 88, 182, 195, 71, 186, 103, 189, 220, 114, 215, 205, 199, 3, 45, 199, 117, 177, 115, 219, 186, 180, 69, 217, 8, 161, 106, 236, 69, 154, 151, 138, 2, 220, 85, 153, 181, 119, 36, 75, 184, 132, 73, 50, 127, 23, 219, 161, 109, 33, 43, 29, 208, 26, 12, 23, 214, 135, 20, 88, 236, 180, 14, 238, 150, 7, 16, 88, 125, 241, 172, 84, 139, 20, 45, 129, 189, 217, 66, 242, 162, 32, 20, 24, 151, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 140, 200, 27, 49, 174, 104, 10, 127, 96, 124, 245, 57, 251, 255, 197, 11, 25, 240, 85, 105, 226, 236, 188, 64, 1, 4, 23, 64, 126, 94, 92, 161, 216, 64, 84, 194, 20, 107, 18, 90, 203, 159, 26, 30, 157, 126, 80, 136, 92, 116, 81, 37, 6, 82, 154, 80, 73, 101, 134, 241, 162, 2, 16 }, new byte[] { 145, 192, 223, 184, 57, 115, 216, 185, 98, 75, 12, 193, 55, 255, 215, 8, 108, 86, 90, 217, 120, 174, 189, 232, 243, 14, 108, 164, 230, 91, 202, 44, 157, 207, 222, 28, 132, 255, 70, 191, 241, 147, 128, 88, 182, 195, 71, 186, 103, 189, 220, 114, 215, 205, 199, 3, 45, 199, 117, 177, 115, 219, 186, 180, 69, 217, 8, 161, 106, 236, 69, 154, 151, 138, 2, 220, 85, 153, 181, 119, 36, 75, 184, 132, 73, 50, 127, 23, 219, 161, 109, 33, 43, 29, 208, 26, 12, 23, 214, 135, 20, 88, 236, 180, 14, 238, 150, 7, 16, 88, 125, 241, 172, 84, 139, 20, 45, 129, 189, 217, 66, 242, 162, 32, 20, 24, 151, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 140, 200, 27, 49, 174, 104, 10, 127, 96, 124, 245, 57, 251, 255, 197, 11, 25, 240, 85, 105, 226, 236, 188, 64, 1, 4, 23, 64, 126, 94, 92, 161, 216, 64, 84, 194, 20, 107, 18, 90, 203, 159, 26, 30, 157, 126, 80, 136, 92, 116, 81, 37, 6, 82, 154, 80, 73, 101, 134, 241, 162, 2, 16 }, new byte[] { 145, 192, 223, 184, 57, 115, 216, 185, 98, 75, 12, 193, 55, 255, 215, 8, 108, 86, 90, 217, 120, 174, 189, 232, 243, 14, 108, 164, 230, 91, 202, 44, 157, 207, 222, 28, 132, 255, 70, 191, 241, 147, 128, 88, 182, 195, 71, 186, 103, 189, 220, 114, 215, 205, 199, 3, 45, 199, 117, 177, 115, 219, 186, 180, 69, 217, 8, 161, 106, 236, 69, 154, 151, 138, 2, 220, 85, 153, 181, 119, 36, 75, 184, 132, 73, 50, 127, 23, 219, 161, 109, 33, 43, 29, 208, 26, 12, 23, 214, 135, 20, 88, 236, 180, 14, 238, 150, 7, 16, 88, 125, 241, 172, 84, 139, 20, 45, 129, 189, 217, 66, 242, 162, 32, 20, 24, 151, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 21, 140, 200, 27, 49, 174, 104, 10, 127, 96, 124, 245, 57, 251, 255, 197, 11, 25, 240, 85, 105, 226, 236, 188, 64, 1, 4, 23, 64, 126, 94, 92, 161, 216, 64, 84, 194, 20, 107, 18, 90, 203, 159, 26, 30, 157, 126, 80, 136, 92, 116, 81, 37, 6, 82, 154, 80, 73, 101, 134, 241, 162, 2, 16 }, new byte[] { 145, 192, 223, 184, 57, 115, 216, 185, 98, 75, 12, 193, 55, 255, 215, 8, 108, 86, 90, 217, 120, 174, 189, 232, 243, 14, 108, 164, 230, 91, 202, 44, 157, 207, 222, 28, 132, 255, 70, 191, 241, 147, 128, 88, 182, 195, 71, 186, 103, 189, 220, 114, 215, 205, 199, 3, 45, 199, 117, 177, 115, 219, 186, 180, 69, 217, 8, 161, 106, 236, 69, 154, 151, 138, 2, 220, 85, 153, 181, 119, 36, 75, 184, 132, 73, 50, 127, 23, 219, 161, 109, 33, 43, 29, 208, 26, 12, 23, 214, 135, 20, 88, 236, 180, 14, 238, 150, 7, 16, 88, 125, 241, 172, 84, 139, 20, 45, 129, 189, 217, 66, 242, 162, 32, 20, 24, 151, 182 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("0f0ea35a-fd23-472b-9bec-086613687b3b"), "Van", "KAY 215H" },
                    { new Guid("27ac1a43-65a5-4a85-9948-7723f24bd979"), "Truck", "KCZ 595L" },
                    { new Guid("35849df7-7218-4dbb-8613-81ec50d400e9"), "Van", "KBF 462A" },
                    { new Guid("3b2c2adb-09f6-4315-b01b-51d549dfca44"), "Truck", "KDE 228S" },
                    { new Guid("79901738-395a-4e06-a38c-6ac4961b3790"), "Truck", "KDL 085M" },
                    { new Guid("c681a498-9424-4b19-8b2a-93e58b3acfcb"), "Truck", "KDB 387Q" },
                    { new Guid("d04ea495-aa7c-443c-bf1d-af06a27b6fd8"), "Truck", "KCY 067A" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("2c1acc53-f918-4d52-be58-f03fa4b385ac"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("4345ae39-6c57-46a5-9379-79226e2e0884"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("4944f6dd-f65e-4cbf-ab80-6e15536f8523"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("4fa2c80e-b1a8-4793-b7d1-0c906becac48"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("563e12d1-808b-4aed-9109-c6a99472c17b"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8973ab59-ecf9-4765-a50e-b976745d001c"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8b2f5372-2dfa-481a-a512-5d17553517ac"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("af1b8bc2-d29f-4a5b-97c2-b4f0e92afa66"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("ca98ce76-0923-4a30-b67a-5dd5fd5d6b15"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f7df347d-07bd-4a26-bd5b-1e2b7ec43a60"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("0f0ea35a-fd23-472b-9bec-086613687b3b"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("27ac1a43-65a5-4a85-9948-7723f24bd979"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("35849df7-7218-4dbb-8613-81ec50d400e9"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("3b2c2adb-09f6-4315-b01b-51d549dfca44"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("79901738-395a-4e06-a38c-6ac4961b3790"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c681a498-9424-4b19-8b2a-93e58b3acfcb"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("d04ea495-aa7c-443c-bf1d-af06a27b6fd8"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Parcels",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wms_android.data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSeedDataWithConsoleMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "LicenseNumber" },
                values: new object[,]
                {
                    { new Guid("447a934e-9e8d-4575-ad2e-ffbe7c883ea6"), "Stephen", "SK", "Kimuyu", "DL123456" },
                    { new Guid("5d88ffb3-28b9-4f05-8e33-6a94641b56b2"), "David", "DK", "Kibet", "DL123456" },
                    { new Guid("5f0e81c8-7366-406d-83fe-9adc7aefb7f3"), "Erastus", "EK", "Kagwa", "DL123456" },
                    { new Guid("685492fb-b1c0-4ec3-94eb-100646e925f9"), "John", "JM", "Mwai", "DL123456" },
                    { new Guid("8d29cb2e-41ab-4d12-9bb7-2538739f19f2"), "James", "SK", "Gichohi", "DL123456" },
                    { new Guid("c5b61f40-f37d-449b-981f-d0cd39e5afd5"), "Julius", "JK", "Kamula", "DL123456" },
                    { new Guid("df811e7a-b41f-4443-91cb-39706c386fdb"), "Charles", "SK", "Maina", "DL123456" },
                    { new Guid("efdaf52d-2786-48c4-8384-890218ec2a2d"), "John", "JN", "Njuguna", "DL123456" },
                    { new Guid("f4c15012-42f9-4745-997c-d39821591fa9"), "David", "DM", "Mwangi", "DL123456" },
                    { new Guid("fc89d46e-c4f1-4f30-9151-3e67a428ee02"), "Robert", "RN", "Njuguna", "DL123456" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 133, 113, 203, 72, 213, 106, 75, 245, 244, 105, 29, 236, 174, 60, 36, 98, 175, 3, 169, 155, 175, 184, 166, 49, 106, 192, 30, 44, 188, 31, 3, 128, 91, 113, 191, 91, 2, 156, 217, 147, 212, 96, 46, 25, 94, 28, 149, 216, 199, 204, 112, 177, 210, 138, 134, 84, 188, 23, 249, 90, 5, 177, 225, 6 }, new byte[] { 205, 132, 216, 242, 47, 42, 129, 252, 25, 82, 51, 106, 50, 221, 167, 185, 91, 8, 231, 230, 120, 144, 167, 235, 211, 248, 9, 225, 248, 36, 127, 125, 144, 116, 70, 149, 78, 5, 229, 225, 246, 157, 11, 57, 59, 58, 27, 89, 69, 151, 165, 89, 177, 17, 236, 166, 57, 214, 133, 194, 200, 44, 108, 139, 164, 14, 249, 15, 73, 159, 234, 43, 176, 208, 71, 139, 182, 69, 221, 190, 211, 8, 176, 213, 62, 97, 49, 225, 26, 83, 188, 49, 183, 86, 233, 77, 190, 128, 95, 24, 81, 79, 212, 232, 41, 206, 24, 155, 253, 197, 96, 80, 193, 6, 245, 96, 86, 218, 221, 22, 29, 44, 28, 73, 88, 89, 232, 142 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 133, 113, 203, 72, 213, 106, 75, 245, 244, 105, 29, 236, 174, 60, 36, 98, 175, 3, 169, 155, 175, 184, 166, 49, 106, 192, 30, 44, 188, 31, 3, 128, 91, 113, 191, 91, 2, 156, 217, 147, 212, 96, 46, 25, 94, 28, 149, 216, 199, 204, 112, 177, 210, 138, 134, 84, 188, 23, 249, 90, 5, 177, 225, 6 }, new byte[] { 205, 132, 216, 242, 47, 42, 129, 252, 25, 82, 51, 106, 50, 221, 167, 185, 91, 8, 231, 230, 120, 144, 167, 235, 211, 248, 9, 225, 248, 36, 127, 125, 144, 116, 70, 149, 78, 5, 229, 225, 246, 157, 11, 57, 59, 58, 27, 89, 69, 151, 165, 89, 177, 17, 236, 166, 57, 214, 133, 194, 200, 44, 108, 139, 164, 14, 249, 15, 73, 159, 234, 43, 176, 208, 71, 139, 182, 69, 221, 190, 211, 8, 176, 213, 62, 97, 49, 225, 26, 83, 188, 49, 183, 86, 233, 77, 190, 128, 95, 24, 81, 79, 212, 232, 41, 206, 24, 155, 253, 197, 96, 80, 193, 6, 245, 96, 86, 218, 221, 22, 29, 44, 28, 73, 88, 89, 232, 142 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 133, 113, 203, 72, 213, 106, 75, 245, 244, 105, 29, 236, 174, 60, 36, 98, 175, 3, 169, 155, 175, 184, 166, 49, 106, 192, 30, 44, 188, 31, 3, 128, 91, 113, 191, 91, 2, 156, 217, 147, 212, 96, 46, 25, 94, 28, 149, 216, 199, 204, 112, 177, 210, 138, 134, 84, 188, 23, 249, 90, 5, 177, 225, 6 }, new byte[] { 205, 132, 216, 242, 47, 42, 129, 252, 25, 82, 51, 106, 50, 221, 167, 185, 91, 8, 231, 230, 120, 144, 167, 235, 211, 248, 9, 225, 248, 36, 127, 125, 144, 116, 70, 149, 78, 5, 229, 225, 246, 157, 11, 57, 59, 58, 27, 89, 69, 151, 165, 89, 177, 17, 236, 166, 57, 214, 133, 194, 200, 44, 108, 139, 164, 14, 249, 15, 73, 159, 234, 43, 176, 208, 71, 139, 182, 69, 221, 190, 211, 8, 176, 213, 62, 97, 49, 225, 26, 83, 188, 49, 183, 86, 233, 77, 190, 128, 95, 24, 81, 79, 212, 232, 41, 206, 24, 155, 253, 197, 96, 80, 193, 6, 245, 96, 86, 218, 221, 22, 29, 44, 28, 73, 88, 89, 232, 142 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 133, 113, 203, 72, 213, 106, 75, 245, 244, 105, 29, 236, 174, 60, 36, 98, 175, 3, 169, 155, 175, 184, 166, 49, 106, 192, 30, 44, 188, 31, 3, 128, 91, 113, 191, 91, 2, 156, 217, 147, 212, 96, 46, 25, 94, 28, 149, 216, 199, 204, 112, 177, 210, 138, 134, 84, 188, 23, 249, 90, 5, 177, 225, 6 }, new byte[] { 205, 132, 216, 242, 47, 42, 129, 252, 25, 82, 51, 106, 50, 221, 167, 185, 91, 8, 231, 230, 120, 144, 167, 235, 211, 248, 9, 225, 248, 36, 127, 125, 144, 116, 70, 149, 78, 5, 229, 225, 246, 157, 11, 57, 59, 58, 27, 89, 69, 151, 165, 89, 177, 17, 236, 166, 57, 214, 133, 194, 200, 44, 108, 139, 164, 14, 249, 15, 73, 159, 234, 43, 176, 208, 71, 139, 182, 69, 221, 190, 211, 8, 176, 213, 62, 97, 49, 225, 26, 83, 188, 49, 183, 86, 233, 77, 190, 128, 95, 24, 81, 79, 212, 232, 41, 206, 24, 155, 253, 197, 96, 80, 193, 6, 245, 96, 86, 218, 221, 22, 29, 44, 28, 73, 88, 89, 232, 142 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 133, 113, 203, 72, 213, 106, 75, 245, 244, 105, 29, 236, 174, 60, 36, 98, 175, 3, 169, 155, 175, 184, 166, 49, 106, 192, 30, 44, 188, 31, 3, 128, 91, 113, 191, 91, 2, 156, 217, 147, 212, 96, 46, 25, 94, 28, 149, 216, 199, 204, 112, 177, 210, 138, 134, 84, 188, 23, 249, 90, 5, 177, 225, 6 }, new byte[] { 205, 132, 216, 242, 47, 42, 129, 252, 25, 82, 51, 106, 50, 221, 167, 185, 91, 8, 231, 230, 120, 144, 167, 235, 211, 248, 9, 225, 248, 36, 127, 125, 144, 116, 70, 149, 78, 5, 229, 225, 246, 157, 11, 57, 59, 58, 27, 89, 69, 151, 165, 89, 177, 17, 236, 166, 57, 214, 133, 194, 200, 44, 108, 139, 164, 14, 249, 15, 73, 159, 234, 43, 176, 208, 71, 139, 182, 69, 221, 190, 211, 8, 176, 213, 62, 97, 49, 225, 26, 83, 188, 49, 183, 86, 233, 77, 190, 128, 95, 24, 81, 79, 212, 232, 41, 206, 24, 155, 253, 197, 96, 80, 193, 6, 245, 96, 86, 218, 221, 22, 29, 44, 28, 73, 88, 89, 232, 142 } });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BodyType", "VehicleRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("36aaf54b-311e-493a-9bf0-8ea6273b3775"), "Truck", "KDE 228S" },
                    { new Guid("630fe42d-7171-4b5f-8f04-d67ecfd225b2"), "Van", "KBF 462A" },
                    { new Guid("6f95459d-35cc-4d8b-9444-75f77baca095"), "Truck", "KCY 067A" },
                    { new Guid("b2c2a35a-ddab-4a3c-be23-4a8cfccd4fdf"), "Truck", "KDB 387Q" },
                    { new Guid("c12d5275-f632-44bd-bc75-4d9ef302b9c8"), "Van", "KAY 215H" },
                    { new Guid("cc9a3d77-25a4-4615-acc4-151907c7a127"), "Truck", "KDL 085M" },
                    { new Guid("e553472d-ee1f-41a0-a2e6-65a57b080274"), "Truck", "KCZ 595L" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("447a934e-9e8d-4575-ad2e-ffbe7c883ea6"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("5d88ffb3-28b9-4f05-8e33-6a94641b56b2"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("5f0e81c8-7366-406d-83fe-9adc7aefb7f3"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("685492fb-b1c0-4ec3-94eb-100646e925f9"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8d29cb2e-41ab-4d12-9bb7-2538739f19f2"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("c5b61f40-f37d-449b-981f-d0cd39e5afd5"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("df811e7a-b41f-4443-91cb-39706c386fdb"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("efdaf52d-2786-48c4-8384-890218ec2a2d"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("f4c15012-42f9-4745-997c-d39821591fa9"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("fc89d46e-c4f1-4f30-9151-3e67a428ee02"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("36aaf54b-311e-493a-9bf0-8ea6273b3775"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("630fe42d-7171-4b5f-8f04-d67ecfd225b2"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6f95459d-35cc-4d8b-9444-75f77baca095"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("b2c2a35a-ddab-4a3c-be23-4a8cfccd4fdf"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("c12d5275-f632-44bd-bc75-4d9ef302b9c8"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("cc9a3d77-25a4-4615-acc4-151907c7a127"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("e553472d-ee1f-41a0-a2e6-65a57b080274"));

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
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class F4AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                 table: "AspNetRoles",
                 columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                 values: new object[] { "4fc5615-0e7f-4a42-1bb5-58e50d9130d0", "Admin", "Admin", "1" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "0fe639f8-c3d9-4f07-b69c-5b85cffdbac7", "Instructor", "Instructor", "2" });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "6f0c8295-bbb8-4ea0-8b36-28e6383588e6", "Student", "Student", "3" });
            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("37527e22-8412-4c02-b979-3afbad3fa14d"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("5ca38c77-4e31-467f-ae70-7c22b275b6b7"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("b5add83b-e034-4664-8b7c-9efc3b50d77d"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("f6b03258-f9aa-46cf-b75b-b1868d58fe3d"));

            migrationBuilder.InsertData(
                table: "TypePoint",
                columns: new[] { "Id", "Coefficient", "CreatedBy", "CreatedDate", "IsDelete", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("32197c6a-0bbb-45db-8a74-8f79e4c69fba"), 2, "Admin", new DateTime(2023, 9, 26, 14, 46, 35, 37, DateTimeKind.Local).AddTicks(6325), false, " Kiểm tra 1 tiết", "Admin", null },
                    { new Guid("37c63650-c628-46da-8750-471ccc67d6fc"), 1, "Admin", new DateTime(2023, 9, 26, 14, 46, 35, 37, DateTimeKind.Local).AddTicks(6316), false, " Kiểm tra 15p", "Admin", null },
                    { new Guid("786a1575-9d55-47dd-b363-6d993ac3d4a0"), 1, "Admin", new DateTime(2023, 9, 26, 14, 46, 35, 37, DateTimeKind.Local).AddTicks(6271), false, " Kiểm tra miệng", "Admin", null },
                    { new Guid("87a844f5-6571-4edf-8671-2ef53865276d"), 3, "Admin", new DateTime(2023, 9, 26, 14, 46, 35, 37, DateTimeKind.Local).AddTicks(6333), false, " Kiểm tra cuối kì", "Admin", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("32197c6a-0bbb-45db-8a74-8f79e4c69fba"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("37c63650-c628-46da-8750-471ccc67d6fc"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("786a1575-9d55-47dd-b363-6d993ac3d4a0"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("87a844f5-6571-4edf-8671-2ef53865276d"));

            migrationBuilder.InsertData(
                table: "TypePoint",
                columns: new[] { "Id", "Coefficient", "CreatedBy", "CreatedDate", "IsDelete", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("37527e22-8412-4c02-b979-3afbad3fa14d"), 1, "Admin", new DateTime(2023, 9, 21, 21, 3, 48, 16, DateTimeKind.Local).AddTicks(99), false, " Kiểm tra miệng", "Admin", null },
                    { new Guid("5ca38c77-4e31-467f-ae70-7c22b275b6b7"), 3, "Admin", new DateTime(2023, 9, 21, 21, 3, 48, 16, DateTimeKind.Local).AddTicks(148), false, " Kiểm tra cuối kì", "Admin", null },
                    { new Guid("b5add83b-e034-4664-8b7c-9efc3b50d77d"), 1, "Admin", new DateTime(2023, 9, 21, 21, 3, 48, 16, DateTimeKind.Local).AddTicks(131), false, " Kiểm tra 15p", "Admin", null },
                    { new Guid("f6b03258-f9aa-46cf-b75b-b1868d58fe3d"), 2, "Admin", new DateTime(2023, 9, 21, 21, 3, 48, 16, DateTimeKind.Local).AddTicks(140), false, " Kiểm tra 1 tiết", "Admin", null }
                });
        }
    }
}

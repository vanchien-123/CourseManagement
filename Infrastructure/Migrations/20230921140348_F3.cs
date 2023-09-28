using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class F3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("2bb84aec-f7cb-49d3-a211-3489d0ade980"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("3eb40557-c3b9-46bb-b5c2-b9fcfe30f72b"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("acb1b111-7b16-4c88-b74a-18c4afbfaff2"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("d6ea4ca3-8a83-4e46-a028-b09fa171d240"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("2bb84aec-f7cb-49d3-a211-3489d0ade980"), 2, "Admin", new DateTime(2023, 9, 21, 0, 44, 41, 311, DateTimeKind.Local).AddTicks(5246), false, " Kiểm tra 1 tiết", "Admin", null },
                    { new Guid("3eb40557-c3b9-46bb-b5c2-b9fcfe30f72b"), 1, "Admin", new DateTime(2023, 9, 21, 0, 44, 41, 311, DateTimeKind.Local).AddTicks(5237), false, " Kiểm tra 15p", "Admin", null },
                    { new Guid("acb1b111-7b16-4c88-b74a-18c4afbfaff2"), 1, "Admin", new DateTime(2023, 9, 21, 0, 44, 41, 311, DateTimeKind.Local).AddTicks(5203), false, " Kiểm tra miệng", "Admin", null },
                    { new Guid("d6ea4ca3-8a83-4e46-a028-b09fa171d240"), 3, "Admin", new DateTime(2023, 9, 21, 0, 44, 41, 311, DateTimeKind.Local).AddTicks(5265), false, " Kiểm tra cuối kì", "Admin", null }
                });
        }
    }
}

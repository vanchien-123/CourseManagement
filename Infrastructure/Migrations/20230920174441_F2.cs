using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class F2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("61b5151e-4bf8-44a0-adc8-533a3d4076a8"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("967afea9-cb63-455d-867a-6bb406ce03a2"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("c1c8c151-9c05-4559-892c-0b722000bd48"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("f01df6f0-ea00-4b24-9377-810cc0f0ff8f"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "TypePoint",
                columns: new[] { "Id", "Coefficient", "CreatedBy", "CreatedDate", "IsDelete", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("61b5151e-4bf8-44a0-adc8-533a3d4076a8"), 1, "Admin", new DateTime(2023, 9, 21, 0, 38, 50, 489, DateTimeKind.Local).AddTicks(1275), false, " Kiểm tra miệng", "Admin", null },
                    { new Guid("967afea9-cb63-455d-867a-6bb406ce03a2"), 3, "Admin", new DateTime(2023, 9, 21, 0, 38, 50, 489, DateTimeKind.Local).AddTicks(1329), false, " Kiểm tra cuối kì", "Admin", null },
                    { new Guid("c1c8c151-9c05-4559-892c-0b722000bd48"), 2, "Admin", new DateTime(2023, 9, 21, 0, 38, 50, 489, DateTimeKind.Local).AddTicks(1321), false, " Kiểm tra 1 tiết", "Admin", null },
                    { new Guid("f01df6f0-ea00-4b24-9377-810cc0f0ff8f"), 1, "Admin", new DateTime(2023, 9, 21, 0, 38, 50, 489, DateTimeKind.Local).AddTicks(1311), false, " Kiểm tra 15p", "Admin", null }
                });
        }
    }
}

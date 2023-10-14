using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class F5AddParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Classroom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "TypePoint",
                columns: new[] { "Id", "Coefficient", "CreatedBy", "CreatedDate", "IsDelete", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("016fdfd1-c2d8-4b22-86d9-8941f917017c"), 1, "Admin", new DateTime(2023, 10, 11, 21, 29, 16, 538, DateTimeKind.Local).AddTicks(9884), false, " Kiểm tra miệng", "Admin", null },
                    { new Guid("0edafdfe-65bf-4b91-894b-4c900b829881"), 3, "Admin", new DateTime(2023, 10, 11, 21, 29, 16, 539, DateTimeKind.Local).AddTicks(3), false, " Kiểm tra cuối kì", "Admin", null },
                    { new Guid("3680eaee-4e87-4c0b-a9de-bd2345ac3fc2"), 2, "Admin", new DateTime(2023, 10, 11, 21, 29, 16, 538, DateTimeKind.Local).AddTicks(9983), false, " Kiểm tra 1 tiết", "Admin", null },
                    { new Guid("af672274-1d55-4931-a373-558fa9832d64"), 1, "Admin", new DateTime(2023, 10, 11, 21, 29, 16, 538, DateTimeKind.Local).AddTicks(9943), false, " Kiểm tra 15p", "Admin", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_StudentId",
                table: "Classroom",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_Student_StudentId",
                table: "Classroom",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_Student_StudentId",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_StudentId",
                table: "Classroom");

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("016fdfd1-c2d8-4b22-86d9-8941f917017c"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("0edafdfe-65bf-4b91-894b-4c900b829881"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("3680eaee-4e87-4c0b-a9de-bd2345ac3fc2"));

            migrationBuilder.DeleteData(
                table: "TypePoint",
                keyColumn: "Id",
                keyValue: new Guid("af672274-1d55-4931-a373-558fa9832d64"));

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}

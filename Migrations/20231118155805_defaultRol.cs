using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForSendKH.Migrations
{
    /// <inheritdoc />
    public partial class defaultRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferts_AspNetUsers_UserId",
                table: "Transferts");

            migrationBuilder.DropIndex(
                name: "IX_Transferts_UserId",
                table: "Transferts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5f05a6e-5c55-4dd1-99bc-df0b1e97e3f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec1848d0-2784-4645-973e-fa6f0e732675");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transferts");

            migrationBuilder.AlterColumn<string>(
                name: "ApiUserId",
                table: "Transferts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62aa44c1-9862-4092-89b5-bfdd242c1acc", null, "User", "USER" },
                    { "a4d7c7ff-d5a2-4ed5-b3b7-252fa867d0bf", null, "Administrateur", "ADMINISTRATEUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferts_ApiUserId",
                table: "Transferts",
                column: "ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferts_AspNetUsers_ApiUserId",
                table: "Transferts",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferts_AspNetUsers_ApiUserId",
                table: "Transferts");

            migrationBuilder.DropIndex(
                name: "IX_Transferts_ApiUserId",
                table: "Transferts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62aa44c1-9862-4092-89b5-bfdd242c1acc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4d7c7ff-d5a2-4ed5-b3b7-252fa867d0bf");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApiUserId",
                table: "Transferts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Transferts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d5f05a6e-5c55-4dd1-99bc-df0b1e97e3f9", null, "User", "USER" },
                    { "ec1848d0-2784-4645-973e-fa6f0e732675", null, "Administrateur", "ADMINISTRATEUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferts_UserId",
                table: "Transferts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferts_AspNetUsers_UserId",
                table: "Transferts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

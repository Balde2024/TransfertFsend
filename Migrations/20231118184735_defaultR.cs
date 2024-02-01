using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForSendKH.Migrations
{
    /// <inheritdoc />
    public partial class defaultR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferts_AspNetUsers_ApiUserId",
                table: "Transferts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a3970b6-e1d3-40a3-a3ca-ad7eb944ca89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e3b8af0-447e-447e-9afc-db7eedf806c3");

            migrationBuilder.RenameColumn(
                name: "ApiUserId",
                table: "Transferts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transferts_ApiUserId",
                table: "Transferts",
                newName: "IX_Transferts_UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00c9b707-289c-4a0c-b99f-436034ee7a53", null, "Administrateur", "ADMINISTRATEUR" },
                    { "a91a6478-c739-48bf-a8a2-578f233f97c2", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transferts_AspNetUsers_UserId",
                table: "Transferts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferts_AspNetUsers_UserId",
                table: "Transferts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00c9b707-289c-4a0c-b99f-436034ee7a53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a91a6478-c739-48bf-a8a2-578f233f97c2");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transferts",
                newName: "ApiUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transferts_UserId",
                table: "Transferts",
                newName: "IX_Transferts_ApiUserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a3970b6-e1d3-40a3-a3ca-ad7eb944ca89", null, "User", "USER" },
                    { "9e3b8af0-447e-447e-9afc-db7eedf806c3", null, "Administrateur", "ADMINISTRATEUR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transferts_AspNetUsers_ApiUserId",
                table: "Transferts",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

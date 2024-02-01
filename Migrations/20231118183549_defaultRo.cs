using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForSendKH.Migrations
{
    /// <inheritdoc />
    public partial class defaultRo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62aa44c1-9862-4092-89b5-bfdd242c1acc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4d7c7ff-d5a2-4ed5-b3b7-252fa867d0bf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a3970b6-e1d3-40a3-a3ca-ad7eb944ca89", null, "User", "USER" },
                    { "9e3b8af0-447e-447e-9afc-db7eedf806c3", null, "Administrateur", "ADMINISTRATEUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a3970b6-e1d3-40a3-a3ca-ad7eb944ca89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e3b8af0-447e-447e-9afc-db7eedf806c3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62aa44c1-9862-4092-89b5-bfdd242c1acc", null, "User", "USER" },
                    { "a4d7c7ff-d5a2-4ed5-b3b7-252fa867d0bf", null, "Administrateur", "ADMINISTRATEUR" }
                });
        }
    }
}

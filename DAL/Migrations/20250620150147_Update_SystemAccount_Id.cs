using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_SystemAccount_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "SystemAccounts",
                columns: new[] { "AccountId", "AccountEmail", "AccountName", "AccountPassword", "AccountRole" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "Admin nek", "123", 3 },
                    { 2, "lecturer@gmail.com", "Lecture nek", "123", 2 },
                    { 3, "staff@gmail.com", "Staff nek", "123", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemAccounts",
                keyColumn: "AccountId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "SystemAccounts",
                columns: new[] { "AccountId", "AccountEmail", "AccountName", "AccountPassword", "AccountRole" },
                values: new object[,]
                {
                    { -3, "staff@gmail.com", "Staff nek", "123", 1 },
                    { -2, "lecturer@gmail.com", "Lecture nek", "123", 2 },
                    { -1, "admin@gmail.com", "Admin nek", "123", 3 }
                });
        }
    }
}

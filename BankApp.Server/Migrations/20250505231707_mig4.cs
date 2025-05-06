using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAccounts_Users_UserId1",
                table: "CompanyAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAccounts_UserId1",
                table: "CompanyAccounts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CompanyAccounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "CompanyAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAccounts_UserId1",
                table: "CompanyAccounts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAccounts_Users_UserId1",
                table: "CompanyAccounts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

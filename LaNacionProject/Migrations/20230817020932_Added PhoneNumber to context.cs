using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaNacionProject.Migrations
{
    public partial class AddedPhoneNumbertocontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Contacts_ContactId",
                table: "PhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber");

            migrationBuilder.RenameTable(
                name: "PhoneNumber",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_Number",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_Number");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_ContactId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_Number",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_Number");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_ContactId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Contacts_ContactId",
                table: "PhoneNumber",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

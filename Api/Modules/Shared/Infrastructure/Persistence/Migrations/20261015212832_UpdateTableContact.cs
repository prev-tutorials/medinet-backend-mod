using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Modules.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_persons_PersonContact_PersonContactId",
                table: "persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonContact",
                table: "PersonContact");

            migrationBuilder.RenameTable(
                name: "PersonContact",
                newName: "person_contacts");

            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "persons",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_contacts",
                table: "person_contacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_persons_person_contacts_PersonContactId",
                table: "persons",
                column: "PersonContactId",
                principalTable: "person_contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_persons_person_contacts_PersonContactId",
                table: "persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_contacts",
                table: "person_contacts");

            migrationBuilder.RenameTable(
                name: "person_contacts",
                newName: "PersonContact");

            migrationBuilder.AlterColumn<int>(
                name: "Dni",
                table: "persons",
                type: "int",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonContact",
                table: "PersonContact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_persons_PersonContact_PersonContactId",
                table: "persons",
                column: "PersonContactId",
                principalTable: "PersonContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

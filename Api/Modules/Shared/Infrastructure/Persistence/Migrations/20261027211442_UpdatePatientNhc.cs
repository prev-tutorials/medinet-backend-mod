using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Modules.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientNhc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nhc",
                table: "patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nhc",
                table: "patients");
        }
    }
}

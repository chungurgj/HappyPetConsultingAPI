using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class ijeroiwjeoir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Started",
                table: "Consultations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartedBy",
                table: "Consultations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Started",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "StartedBy",
                table: "Consultations");
        }
    }
}

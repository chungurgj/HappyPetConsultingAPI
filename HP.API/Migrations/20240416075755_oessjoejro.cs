using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class oessjoejro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Possible",
                table: "Consultations");

            migrationBuilder.AddColumn<bool>(
                name: "Possible",
                table: "Slots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Possible",
                table: "Slots");

            migrationBuilder.AddColumn<bool>(
                name: "Possible",
                table: "Consultations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

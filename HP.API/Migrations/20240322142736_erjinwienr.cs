using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class erjinwienr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner_Email",
                table: "TextConsultations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pet_Age",
                table: "TextConsultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Pet_Breed",
                table: "TextConsultations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner_Email",
                table: "TextConsultations");

            migrationBuilder.DropColumn(
                name: "Pet_Age",
                table: "TextConsultations");

            migrationBuilder.DropColumn(
                name: "Pet_Breed",
                table: "TextConsultations");
        }
    }
}

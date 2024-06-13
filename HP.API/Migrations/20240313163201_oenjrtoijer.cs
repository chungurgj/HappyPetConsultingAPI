using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class oenjrtoijer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Owner_Id",
                table: "TextConsultations",
                newName: "Vet_Id");

            migrationBuilder.AddColumn<string>(
                name: "Owner_Name",
                table: "TextConsultations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner_Name",
                table: "TextConsultations");

            migrationBuilder.RenameColumn(
                name: "Vet_Id",
                table: "TextConsultations",
                newName: "Owner_Id");
        }
    }
}

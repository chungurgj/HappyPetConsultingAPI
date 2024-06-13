using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class asdoine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pet_Name",
                table: "VetVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pet_Name",
                table: "VetVisits");
        }
    }
}

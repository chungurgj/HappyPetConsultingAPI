using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class asdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Pets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Pets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}

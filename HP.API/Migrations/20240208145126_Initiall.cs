using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class Initiall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId1",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId1",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Pets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Owner_Id",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Owner_Id",
                table: "Pets");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Pets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId1",
                table: "Pets",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_IdentityUser_OwnerId1",
                table: "Pets",
                column: "OwnerId1",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}

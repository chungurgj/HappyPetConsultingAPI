using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class ie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VetVisits_Pets_PetId",
                table: "VetVisits");

            migrationBuilder.DropIndex(
                name: "IX_VetVisits_PetId",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "VetVisits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PetId",
                table: "VetVisits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VetVisits_PetId",
                table: "VetVisits",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_VetVisits_Pets_PetId",
                table: "VetVisits",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

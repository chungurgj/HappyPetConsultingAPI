using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class pomwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "Pet_Name",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "VisitEnd",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "VisitStart",
                table: "VetVisits");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeEnd",
                table: "VetVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeStart",
                table: "VetVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "VetVisits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PetId",
                table: "VetVisits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VetVisits_OwnerId",
                table: "VetVisits",
                column: "OwnerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VetVisits_User_OwnerId",
                table: "VetVisits",
                column: "OwnerId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VetVisits_Pets_PetId",
                table: "VetVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_VetVisits_User_OwnerId",
                table: "VetVisits");

            migrationBuilder.DropIndex(
                name: "IX_VetVisits_OwnerId",
                table: "VetVisits");

            migrationBuilder.DropIndex(
                name: "IX_VetVisits_PetId",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "DateTimeEnd",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "DateTimeStart",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "VetVisits");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "VetVisits");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "VetVisits",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Pet_Name",
                table: "VetVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "VisitEnd",
                table: "VetVisits",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "VisitStart",
                table: "VetVisits",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}

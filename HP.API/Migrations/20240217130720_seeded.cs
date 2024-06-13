using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class seeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Appointments");

            migrationBuilder.AlterColumn<Guid>(
                name: "Pet_Id",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ApCategoryId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AppointmentsCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppointmentsCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9b486005-84e6-4469-a781-01772691fb0c"), "Emergency" },
                    { new Guid("bbd1ae96-c2e7-48dc-856b-af9c89bd5387"), "Text" },
                    { new Guid("c30e9854-e5e8-4dd0-97fa-8e76986b0385"), "Video" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ApCategoryId",
                table: "Appointments",
                column: "ApCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentsCategories_ApCategoryId",
                table: "Appointments",
                column: "ApCategoryId",
                principalTable: "AppointmentsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentsCategories_ApCategoryId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AppointmentsCategories");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ApCategoryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApCategoryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "Pet_Id",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

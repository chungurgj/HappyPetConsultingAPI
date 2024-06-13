using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class opkkjOIoiOIio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AppointmentsCategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsultationStart",
                table: "Consultations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ConsultationStart",
                table: "Consultations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Owner_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pet_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppointmentsCategories",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("9b486005-84e6-4469-a781-01772691fb0c"), "Emergency", 1200 },
                    { new Guid("c30e9854-e5e8-4dd0-97fa-8e76986b0385"), "Video", 600 }
                });
        }
    }
}

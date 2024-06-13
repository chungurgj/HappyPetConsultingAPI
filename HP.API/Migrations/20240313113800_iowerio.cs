using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class iowerio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentsCategories",
                keyColumn: "Id",
                keyValue: new Guid("bbd1ae96-c2e7-48dc-856b-af9c89bd5387"));

            migrationBuilder.CreateTable(
                name: "TextConsultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pet_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextConsultations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextConsultations");

            migrationBuilder.InsertData(
                table: "AppointmentsCategories",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("bbd1ae96-c2e7-48dc-856b-af9c89bd5387"), "Text", 300 });
        }
    }
}

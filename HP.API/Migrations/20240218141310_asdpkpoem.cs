using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class asdpkpoem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "AppointmentsCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppointmentsCategories",
                keyColumn: "Id",
                keyValue: new Guid("9b486005-84e6-4469-a781-01772691fb0c"),
                column: "Price",
                value: 1200);

            migrationBuilder.UpdateData(
                table: "AppointmentsCategories",
                keyColumn: "Id",
                keyValue: new Guid("bbd1ae96-c2e7-48dc-856b-af9c89bd5387"),
                column: "Price",
                value: 300);

            migrationBuilder.UpdateData(
                table: "AppointmentsCategories",
                keyColumn: "Id",
                keyValue: new Guid("c30e9854-e5e8-4dd0-97fa-8e76986b0385"),
                column: "Price",
                value: 600);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "AppointmentsCategories");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

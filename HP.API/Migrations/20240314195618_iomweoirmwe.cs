using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class iomweoirmwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentsCategories_ApCategoryId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ApCategoryId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ApCategoryId",
                table: "Appointments",
                newName: "Category_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Appointments",
                newName: "ApCategoryId");

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
    }
}

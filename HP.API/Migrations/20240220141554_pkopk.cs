using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class pkopk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VetVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pet_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    VisitStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    VisitEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VetVisits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "VetVisits");
        }
    }
}

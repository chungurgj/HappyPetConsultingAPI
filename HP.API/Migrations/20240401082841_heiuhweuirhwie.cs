using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HP.API.Migrations
{
    /// <inheritdoc />
    public partial class heiuhweuirhwie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextConsultations");

            migrationBuilder.CreateTable(
                name: "ConsultationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pet_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vet_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultationTypeId = table.Column<int>(type: "int", nullable: false),
                    ConsultationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pet_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Age = table.Column<int>(type: "int", nullable: false),
                    Vet_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_ConsultationTypes_ConsultationTypeId",
                        column: x => x.ConsultationTypeId,
                        principalTable: "ConsultationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConsultationTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "text", 300 },
                    { 2, "video", 600 },
                    { 3, "emergency", 900 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_ConsultationTypeId",
                table: "Consultations",
                column: "ConsultationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "ConsultationTypes");

            migrationBuilder.CreateTable(
                name: "TextConsultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Owner_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Age = table.Column<int>(type: "int", nullable: false),
                    Pet_Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pet_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pet_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Vet_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextConsultations", x => x.Id);
                });
        }
    }
}

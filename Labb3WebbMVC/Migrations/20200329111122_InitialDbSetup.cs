using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3WebbMVC.Migrations
{
    public partial class InitialDbSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalonList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    RemainingSeats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Duration = table.Column<double>(nullable: false),
                    StartingTime = table.Column<DateTime>(nullable: false),
                    SalonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieList_SalonList_SalonId",
                        column: x => x.SalonId,
                        principalTable: "SalonList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_SalonId",
                table: "MovieList",
                column: "SalonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieList");

            migrationBuilder.DropTable(
                name: "SalonList");
        }
    }
}

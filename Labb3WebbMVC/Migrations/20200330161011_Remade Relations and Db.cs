using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3WebbMVC.Migrations
{
    public partial class RemadeRelationsandDb : Migration
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
                    SeatCapacity = table.Column<int>(nullable: false),
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
                    Duration = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Viewing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viewing_MovieList_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MovieList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_SalonId",
                table: "MovieList",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Viewing_MovieId",
                table: "Viewing",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Viewing");

            migrationBuilder.DropTable(
                name: "MovieList");

            migrationBuilder.DropTable(
                name: "SalonList");
        }
    }
}

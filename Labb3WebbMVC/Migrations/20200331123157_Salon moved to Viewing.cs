using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3WebbMVC.Migrations
{
    public partial class SalonmovedtoViewing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieList_SalonList_SalonId",
                table: "MovieList");

            migrationBuilder.DropIndex(
                name: "IX_MovieList_SalonId",
                table: "MovieList");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "MovieList");

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Viewing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewingId",
                table: "MovieList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Viewing_SalonId",
                table: "Viewing",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Viewing_SalonList_SalonId",
                table: "Viewing",
                column: "SalonId",
                principalTable: "SalonList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viewing_SalonList_SalonId",
                table: "Viewing");

            migrationBuilder.DropIndex(
                name: "IX_Viewing_SalonId",
                table: "Viewing");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Viewing");

            migrationBuilder.DropColumn(
                name: "ViewingId",
                table: "MovieList");

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "MovieList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_SalonId",
                table: "MovieList",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieList_SalonList_SalonId",
                table: "MovieList",
                column: "SalonId",
                principalTable: "SalonList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

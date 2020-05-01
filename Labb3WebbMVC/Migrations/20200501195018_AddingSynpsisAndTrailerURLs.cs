using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3WebbMVC.Migrations
{
    public partial class AddingSynpsisAndTrailerURLs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MovieList",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "MovieList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerURL",
                table: "MovieList",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "MovieList");

            migrationBuilder.DropColumn(
                name: "TrailerURL",
                table: "MovieList");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MovieList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3WebbMVC.Migrations
{
    public partial class Fixingsnapshots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewingId",
                table: "MovieList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewingId",
                table: "MovieList",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

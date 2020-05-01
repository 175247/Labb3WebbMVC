using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb3WebbMVC.Migrations
{
    public partial class AddedFKtoMovieTitleinViewing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieTitle",
                table: "Viewing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieTitle",
                table: "Viewing");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class LibranzaNormaLegal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormaLegal",
                table: "Libranzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormaLegal",
                table: "Libranzas");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class ObjetoLibranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Objeto",
                table: "Libranzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Objeto",
                table: "Libranzas");
        }
    }
}

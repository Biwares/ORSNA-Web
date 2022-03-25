using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class ObjetoDGLibranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObjetoDatosGenerales",
                table: "Libranzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjetoDatosGenerales",
                table: "Libranzas");
        }
    }
}

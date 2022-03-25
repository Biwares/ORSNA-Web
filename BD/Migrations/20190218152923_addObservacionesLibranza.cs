using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class addObservacionesLibranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Libranzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Libranzas");
        }
    }
}

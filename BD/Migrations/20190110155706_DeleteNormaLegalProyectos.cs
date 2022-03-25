using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class DeleteNormaLegalProyectos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormaLegal",
                table: "Proyectos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormaLegal",
                table: "Proyectos",
                maxLength: 500,
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class PagosImpuestosIncluidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PagosImpuestosIncluidos",
                table: "Proyectos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagosImpuestosIncluidos",
                table: "Proyectos");
        }
    }
}

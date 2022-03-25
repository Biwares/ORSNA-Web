using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class MontoPagadoAniosAnteriores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontoPagadoAniosAnteriores",
                table: "Proyectos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoPagadoAniosAnteriores",
                table: "Proyectos");
        }
    }
}

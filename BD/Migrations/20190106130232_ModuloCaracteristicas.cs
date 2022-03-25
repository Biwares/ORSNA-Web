using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class ModuloCaracteristicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icono",
                table: "Modulos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouterLink",
                table: "Modulos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icono",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "RouterLink",
                table: "Modulos");
        }
    }
}

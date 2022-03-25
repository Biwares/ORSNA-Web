using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class LibranzasmulticesionesbeneficiarioNroEscritura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NroEscritura",
                table: "LibranzaBeneficiariosCesiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NroEscritura",
                table: "LibranzaBeneficiariosCesiones");
        }
    }
}

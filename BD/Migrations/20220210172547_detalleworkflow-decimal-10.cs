using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class detalleworkflowdecimal10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TasaDeCambioActual",
                table: "LibranzaDetalleWorkflow",
                type: "decimal(18, 10)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TasaDeCambioActual",
                table: "LibranzaDetalleWorkflow",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 10)");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class UpdateDetalleWorkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update LibranzaDetalleWorkflow set tasadecambioactual = 1, monedaactualid = 1 where Observaciones = 'Nueva Libranza' and IdEstadoAnterior = 1 and IdNuevoEstado = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

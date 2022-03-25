using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class EstadoProyectoCambioNombre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Update [dbo].[ProyectosEstado] set [Nombre] = 'NUEVO / EN EJECUCIÓN' where [Nombre] ='EN EJECUCIÓN'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

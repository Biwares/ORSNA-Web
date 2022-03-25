using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class EstadoProyectoCambioNombre2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Update [dbo].[ProyectosEstado] set [Nombre] = 'EN EJECUCIÓN' where [Nombre] ='NUEVO / EN EJECUCIÓN'");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ProyectosEstado] (nombre, estado) VALUES ('NUEVO', 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class RevemeNuevoProyectoEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Delete ProyectosEstado where Nombre = 'NUEVO PROYECTO'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ProyectosEstado]([Nombre],[Estado])VALUES('NUEVO PROYECTO',1)");
        }
    }
}

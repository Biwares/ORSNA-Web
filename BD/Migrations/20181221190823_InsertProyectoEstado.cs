using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class InsertProyectoEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ProyectosEstado]([Nombre],[Estado])VALUES('NUEVO PROYECTO',1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Delete ProyectosEstado where Nombre = 'NUEVO PROYECTO'");
        }
    }
}

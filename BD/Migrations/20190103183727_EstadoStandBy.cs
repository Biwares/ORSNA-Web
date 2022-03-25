using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class EstadoStandBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[ProyectosEstado]([Nombre],[Estado])VALUES('STAND BY',1)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Delete ProyectosEstado where Nombre = 'STAND BY'");

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class InsertDestinos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Destinos] (nombre, RequiereAeropuerto, estado) VALUES ('SNA', 0, 1)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Destinos] (nombre, RequiereAeropuerto, estado) VALUES ('Aeropuertos', 1, 1)");
            migrationBuilder.Sql(@"Update [dbo].[Proyectos] set [DestinosId] = 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class moduloEditarLibranzasPagadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[modulos]([Nombre],[Estado])VALUES('Editar Libranzas Pagadas',1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Delete from modulos where Nombre = 'Editar Libranzas Pagadas'");
        }
    }
}

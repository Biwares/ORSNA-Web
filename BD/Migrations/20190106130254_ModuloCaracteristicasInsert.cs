using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class ModuloCaracteristicasInsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'libranzas', icono = 'fa fa-money' where nombre = 'Libranzas de Pago'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'beneficiarios', icono = 'fa fa-fw  fa-plus-square' where nombre = 'Beneficiarios'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'cuentas', icono = 'fa fa-users' where nombre = 'Cuentas'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'proyectos', icono = 'fa fa-folder-open' where nombre = 'Proyectos'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'areas', icono = 'fa fa-download' where nombre = 'Áreas'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'audit', icono = 'fa fa-file-text-o' where nombre = 'Auditoría'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'usuarios', icono = 'fa fa-user-circle-o' where nombre = 'Usuarios'");
            migrationBuilder.Sql(@"update [dbo].[modulos] set routerlink = 'roles', icono = 'fa fa-user-circle-o' where nombre = 'Roles'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

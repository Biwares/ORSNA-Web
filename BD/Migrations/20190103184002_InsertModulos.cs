using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class InsertModulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Libranzas de Pago',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Beneficiarios',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Cuentas',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Proyectos',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Áreas',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Auditoría',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Usuarios',1,null)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Modulos]([Nombre],[Estado],[RolModuloId])VALUES('Roles',1,null)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Libranzas de Pago'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Beneficiarios'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Cuentas'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Proyectos'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Áreas'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Auditoría'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Usuarios'");
            migrationBuilder.Sql(@"Delete Modulos where Nombre = 'Roles'");
        }
    }
}

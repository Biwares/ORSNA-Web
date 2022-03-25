using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class CodigoProyectoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"With UpdateData  As (SELECT id,ROW_NUMBER() OVER (ORDER BY id) AS RN FROM proyectos) UPDATE proyectos SET codigo = RN FROM proyectos INNER JOIN UpdateData ON proyectos.id = UpdateData.id");
            migrationBuilder.Sql(@"With UpdateData  As(SELECT id,ROW_NUMBER() OVER (ORDER BY id) AS RN FROM proyectos) UPDATE proyectos SET idproyecto = CONCAT((select Codigo from areas a where a.id = proyectos.idarea), ' - ', proyectos.codigo, ' - ', (select Descripcion from Cuentas a where a.id = proyectos.IdCuenta)) FROM proyectos INNER JOIN UpdateData ON proyectos.id = UpdateData.id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

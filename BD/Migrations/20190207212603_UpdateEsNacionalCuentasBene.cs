using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class UpdateEsNacionalCuentasBene : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update BeneficiarioBancos set esnacional = 0 where IdBeneficiario in (select id from Beneficiarios where NacionalExtranjero = 'false')");
            migrationBuilder.Sql(@"update BeneficiarioBancos set esnacional = 1 where IdBeneficiario in (select id from Beneficiarios where NacionalExtranjero = 'true')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

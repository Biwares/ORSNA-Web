using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class RegistraCesionNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update Libranzas set RegistraCesion = 0 where RegistraCesion is null");
            migrationBuilder.Sql(@"update Libranzas set NroEscritura = null, Beneficiario = null, IdentificacionTributaria = null where RegistraCesion = 0");
            
            migrationBuilder.AlterColumn<bool>(
                name: "RegistraCesion",
                table: "Libranzas",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true, defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "RegistraCesion",
                table: "Libranzas",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}

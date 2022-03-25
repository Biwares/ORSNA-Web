using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class InsertMoneda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT Moneda ON");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Moneda] (id, nombre, nombrecorto, estado) VALUES (1, 'Peso argentino', 'AR$', 1)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Moneda] (id, nombre, nombrecorto, estado) VALUES (2, 'Dólar estadounidense', 'USD', 1)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Moneda] (id, nombre, nombrecorto, estado) VALUES (3, 'Dólar canadiens', 'CAD USD', 1)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Moneda] (id, nombre, nombrecorto, estado) VALUES (4, 'Euro', 'EUR', 1)");
            migrationBuilder.Sql(@"SET IDENTITY_INSERT Moneda ON");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

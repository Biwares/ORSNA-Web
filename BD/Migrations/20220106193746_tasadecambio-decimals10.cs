using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class tasadecambiodecimals10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TasaDeCambio",
                table: "Libranzas",
                type: "decimal(18, 10)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TasaDeCambio",
                table: "Libranzas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 10)");
        }
    }
}

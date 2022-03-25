using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class MonedaNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE LIBRANZAS SET MONEDAID = 1");

            migrationBuilder.DropForeignKey(
                name: "FK_Libranzas_Moneda_MonedaId",
                table: "Libranzas");

            migrationBuilder.AlterColumn<int>(
                name: "MonedaId",
                table: "Libranzas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Libranzas_Moneda_MonedaId",
                table: "Libranzas",
                column: "MonedaId",
                principalTable: "Moneda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libranzas_Moneda_MonedaId",
                table: "Libranzas");

            migrationBuilder.AlterColumn<int>(
                name: "MonedaId",
                table: "Libranzas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Libranzas_Moneda_MonedaId",
                table: "Libranzas",
                column: "MonedaId",
                principalTable: "Moneda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

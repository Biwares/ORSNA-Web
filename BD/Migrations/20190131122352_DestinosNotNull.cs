using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class DestinosNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Destinos_DestinosId",
                table: "Proyectos");

            migrationBuilder.AlterColumn<int>(
                name: "DestinosId",
                table: "Proyectos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Destinos_DestinosId",
                table: "Proyectos",
                column: "DestinosId",
                principalTable: "Destinos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Destinos_DestinosId",
                table: "Proyectos");

            migrationBuilder.AlterColumn<int>(
                name: "DestinosId",
                table: "Proyectos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Destinos_DestinosId",
                table: "Proyectos",
                column: "DestinosId",
                principalTable: "Destinos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

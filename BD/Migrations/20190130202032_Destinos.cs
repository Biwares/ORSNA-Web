using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class Destinos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinosId",
                table: "Proyectos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Destinos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    RequiereAeropuerto = table.Column<bool>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_DestinosId",
                table: "Proyectos",
                column: "DestinosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Destinos_DestinosId",
                table: "Proyectos",
                column: "DestinosId",
                principalTable: "Destinos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Destinos_DestinosId",
                table: "Proyectos");

            migrationBuilder.DropTable(
                name: "Destinos");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_DestinosId",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "DestinosId",
                table: "Proyectos");
        }
    }
}

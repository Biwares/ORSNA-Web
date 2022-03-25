using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class MonedaTasaLibranzas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonedaId",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TasaDeCambio",
                table: "Libranzas",
                nullable: false,
                defaultValue: 1m);

            migrationBuilder.CreateTable(
                name: "Moneda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    NombreCorto = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moneda", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libranzas_MonedaId",
                table: "Libranzas",
                column: "MonedaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libranzas_Moneda_MonedaId",
                table: "Libranzas",
                column: "MonedaId",
                principalTable: "Moneda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libranzas_Moneda_MonedaId",
                table: "Libranzas");

            migrationBuilder.DropTable(
                name: "Moneda");

            migrationBuilder.DropIndex(
                name: "IX_Libranzas_MonedaId",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "MonedaId",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "TasaDeCambio",
                table: "Libranzas");
        }
    }
}

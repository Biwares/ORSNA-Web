using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class Libranzasmulticesionesbeneficiario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibranzaBeneficiariosCesiones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranzas = table.Column<int>(nullable: false),
                    IdBeneficiario = table.Column<int>(nullable: false),
                    IdBeneficiarioBancos = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: true),
                    Monto = table.Column<decimal>(nullable: true),
                    IdBeneficiarioBancosNavigationId = table.Column<int>(nullable: true),
                    IdBeneficiarioNavigationId = table.Column<int>(nullable: true),
                    IdLibranzasNavigationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaBeneficiariosCesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaBeneficiariosCesiones_BeneficiarioBancos_IdBeneficiarioBancosNavigationId",
                        column: x => x.IdBeneficiarioBancosNavigationId,
                        principalTable: "BeneficiarioBancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaBeneficiariosCesiones_Beneficiarios_IdBeneficiarioNavigationId",
                        column: x => x.IdBeneficiarioNavigationId,
                        principalTable: "Beneficiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaBeneficiariosCesiones_Libranzas_IdLibranzasNavigationId",
                        column: x => x.IdLibranzasNavigationId,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaBeneficiariosCesiones_IdBeneficiarioBancosNavigationId",
                table: "LibranzaBeneficiariosCesiones",
                column: "IdBeneficiarioBancosNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaBeneficiariosCesiones_IdBeneficiarioNavigationId",
                table: "LibranzaBeneficiariosCesiones",
                column: "IdBeneficiarioNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaBeneficiariosCesiones_IdLibranzasNavigationId",
                table: "LibranzaBeneficiariosCesiones",
                column: "IdLibranzasNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibranzaBeneficiariosCesiones");
        }
    }
}

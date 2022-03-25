using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class LibranzaAdjuntos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibranzaAdjuntos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLibranza = table.Column<int>(nullable: true),
                    IdAdjunto = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibranzaAdjuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibranzaAdjuntos_Adjuntos",
                        column: x => x.IdAdjunto,
                        principalTable: "Adjuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibranzaAdjuntos_Libranzas",
                        column: x => x.IdLibranza,
                        principalTable: "Libranzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaAdjuntos_IdAdjunto",
                table: "LibranzaAdjuntos",
                column: "IdAdjunto");

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaAdjuntos_IdLibranza",
                table: "LibranzaAdjuntos",
                column: "IdLibranza");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibranzaAdjuntos");
        }
    }
}

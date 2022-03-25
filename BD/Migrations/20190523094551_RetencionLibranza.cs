using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class RetencionLibranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Retencion",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RetencionObservaciones",
                table: "Libranzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retencion",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "RetencionObservaciones",
                table: "Libranzas");
        }
    }
}

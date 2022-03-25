using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class TasaDecAmbioDetalleWorkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonedaActualId",
                table: "LibranzaDetalleWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TasaDeCambioActual",
                table: "LibranzaDetalleWorkflow",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_LibranzaDetalleWorkflow_MonedaActualId",
                table: "LibranzaDetalleWorkflow",
                column: "MonedaActualId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibranzaDetalleWorkflow_Moneda_MonedaActualId",
                table: "LibranzaDetalleWorkflow",
                column: "MonedaActualId",
                principalTable: "Moneda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibranzaDetalleWorkflow_Moneda_MonedaActualId",
                table: "LibranzaDetalleWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_LibranzaDetalleWorkflow_MonedaActualId",
                table: "LibranzaDetalleWorkflow");

            migrationBuilder.DropColumn(
                name: "MonedaActualId",
                table: "LibranzaDetalleWorkflow");

            migrationBuilder.DropColumn(
                name: "TasaDeCambioActual",
                table: "LibranzaDetalleWorkflow");
        }
    }
}

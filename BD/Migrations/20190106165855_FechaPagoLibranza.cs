using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class FechaPagoLibranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "LibranzaDetalleWorkflow",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "LibranzaDetalleWorkflow");
        }
    }
}

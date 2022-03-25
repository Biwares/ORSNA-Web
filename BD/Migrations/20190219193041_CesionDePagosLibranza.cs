using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class CesionDePagosLibranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CesionCbu",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CesionCuit",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CesionFechaVigencia",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CesionNombreBanco",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CesionNroCuenta",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CesionSucursal",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CesionTipoCuenta",
                table: "Libranzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CesionTitular",
                table: "Libranzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CesionCbu",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionCuit",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionFechaVigencia",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionNombreBanco",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionNroCuenta",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionSucursal",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionTipoCuenta",
                table: "Libranzas");

            migrationBuilder.DropColumn(
                name: "CesionTitular",
                table: "Libranzas");
        }
    }
}

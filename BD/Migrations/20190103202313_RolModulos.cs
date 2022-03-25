using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class RolModulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_RolModulo_RolModuloId",
                table: "Modulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Rol_RolModulo_RolModuloId",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Rol_RolModuloId",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_RolModuloId",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "RolModuloId",
                table: "Rol");

            migrationBuilder.DropColumn(
                name: "RolModuloId",
                table: "Modulos");

            migrationBuilder.CreateIndex(
                name: "IX_RolModulo_IdModulo",
                table: "RolModulo",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_RolModulo_IdRol",
                table: "RolModulo",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_RolModulo_Modulos_IdModulo",
                table: "RolModulo",
                column: "IdModulo",
                principalTable: "Modulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolModulo_Rol_IdRol",
                table: "RolModulo",
                column: "IdRol",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolModulo_Modulos_IdModulo",
                table: "RolModulo");

            migrationBuilder.DropForeignKey(
                name: "FK_RolModulo_Rol_IdRol",
                table: "RolModulo");

            migrationBuilder.DropIndex(
                name: "IX_RolModulo_IdModulo",
                table: "RolModulo");

            migrationBuilder.DropIndex(
                name: "IX_RolModulo_IdRol",
                table: "RolModulo");

            migrationBuilder.AddColumn<int>(
                name: "RolModuloId",
                table: "Rol",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RolModuloId",
                table: "Modulos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rol_RolModuloId",
                table: "Rol",
                column: "RolModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_RolModuloId",
                table: "Modulos",
                column: "RolModuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_RolModulo_RolModuloId",
                table: "Modulos",
                column: "RolModuloId",
                principalTable: "RolModulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rol_RolModulo_RolModuloId",
                table: "Rol",
                column: "RolModuloId",
                principalTable: "RolModulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

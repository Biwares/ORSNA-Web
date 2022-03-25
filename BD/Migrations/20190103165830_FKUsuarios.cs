using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class FKUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuariosId",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UsuariosId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "UsuarioRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdUsuario",
                table: "UsuarioRol",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_IdRol",
                table: "UsuarioRol",
                column: "IdRol",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_IdUsuario",
                table: "UsuarioRol",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_IdRol",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_IdUsuario",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_IdUsuario",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<int>(
                name: "UsuariosId",
                table: "UsuarioRol",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuariosId",
                table: "UsuarioRol",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuariosId",
                table: "UsuarioRol",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

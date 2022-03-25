using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class UserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolModuloId",
                table: "Modulos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RolModulo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRol = table.Column<int>(nullable: false),
                    IdModulo = table.Column<int>(nullable: false),
                    Ver = table.Column<bool>(nullable: false),
                    Editar = table.Column<bool>(nullable: false),
                    Eliminar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolModulo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    UsuariosId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: true),
                    RolModuloId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rol_RolModulo_RolModuloId",
                        column: x => x.RolModuloId,
                        principalTable: "RolModulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_RolModuloId",
                table: "Modulos",
                column: "RolModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_RolModuloId",
                table: "Rol",
                column: "RolModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuariosId",
                table: "UsuarioRol",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_RolModulo_RolModuloId",
                table: "Modulos",
                column: "RolModuloId",
                principalTable: "RolModulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_RolModulo_RolModuloId",
                table: "Modulos");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "RolModulo");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_RolModuloId",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "RolModuloId",
                table: "Modulos");
        }
    }
}

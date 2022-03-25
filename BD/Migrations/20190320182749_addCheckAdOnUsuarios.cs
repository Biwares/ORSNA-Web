using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class addCheckAdOnUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckAD",
                table: "Usuarios",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckAD",
                table: "Usuarios");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class AddUserNameLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Log",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Log");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BD.Migrations
{
    public partial class MonedaNotNullDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE dbo.Libranzas ADD CONSTRAINT DF_Libranzas_MonedaId DEFAULT 1 FOR MonedaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

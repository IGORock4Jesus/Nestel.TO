using Microsoft.EntityFrameworkCore.Migrations;

namespace Nestel.TO.Migrations
{
    public partial class AddedAddressToObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Objects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Objects");
        }
    }
}

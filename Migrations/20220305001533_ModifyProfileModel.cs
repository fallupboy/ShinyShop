using Microsoft.EntityFrameworkCore.Migrations;

namespace ShinyShop.Migrations
{
    public partial class ModifyProfileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Profiles",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Profiles",
                newName: "ProfileId");
        }
    }
}

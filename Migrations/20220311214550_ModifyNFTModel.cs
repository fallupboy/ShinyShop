using Microsoft.EntityFrameworkCore.Migrations;

namespace ShinyShop.Migrations
{
    public partial class ModifyNFTModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "NFTs",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "NFTs",
                newName: "Name");
        }
    }
}

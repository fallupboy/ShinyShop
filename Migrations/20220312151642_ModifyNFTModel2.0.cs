using Microsoft.EntityFrameworkCore.Migrations;

namespace ShinyShop.Migrations
{
    public partial class ModifyNFTModel20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "NFTs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NFTs_UserId",
                table: "NFTs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NFTs_Users_UserId",
                table: "NFTs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NFTs_Users_UserId",
                table: "NFTs");

            migrationBuilder.DropIndex(
                name: "IX_NFTs_UserId",
                table: "NFTs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NFTs");
        }
    }
}

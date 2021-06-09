using Microsoft.EntityFrameworkCore.Migrations;

namespace ZnanyTrener.API.Migrations
{
    public partial class PhotoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoCloudinaryPublicId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoCloudinaryPublicId",
                table: "AspNetUsers");
        }
    }
}

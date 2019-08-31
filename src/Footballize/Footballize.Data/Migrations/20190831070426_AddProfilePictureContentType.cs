using Microsoft.EntityFrameworkCore.Migrations;

namespace Footballize.Data.Migrations
{
    public partial class AddProfilePictureContentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ProfilePicture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ProfilePicture");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Footballize.Data.Migrations
{
    public partial class AddProfilePictureContentType1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "ProfilePicture",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "ProfilePicture",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}

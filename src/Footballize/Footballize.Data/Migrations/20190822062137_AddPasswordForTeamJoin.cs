using Microsoft.EntityFrameworkCore.Migrations;

namespace Footballize.Data.Migrations
{
    public partial class AddPasswordForTeamJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teams");
        }
    }
}

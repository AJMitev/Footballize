using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Footballize.Data.Migrations
{
    public partial class AddingVersusEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Versuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    StartingAt = table.Column<DateTime>(nullable: false),
                    PitchId = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    AwayTeamId = table.Column<string>(nullable: true),
                    HomeTeamId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versuses_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Versuses_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Versuses_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Versuses_Pitches_PitchId",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Versuses_AwayTeamId",
                table: "Versuses",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Versuses_CreatorId",
                table: "Versuses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Versuses_HomeTeamId",
                table: "Versuses",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Versuses_IsDeleted",
                table: "Versuses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Versuses_PitchId",
                table: "Versuses",
                column: "PitchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Versuses");
        }
    }
}

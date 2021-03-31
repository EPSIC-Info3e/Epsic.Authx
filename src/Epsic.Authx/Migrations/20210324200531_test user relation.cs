using Microsoft.EntityFrameworkCore.Migrations;

namespace Epsic.Authx.Migrations
{
    public partial class testuserrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestsCovid",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestsCovid_UserId",
                table: "TestsCovid",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestsCovid_AspNetUsers_UserId",
                table: "TestsCovid",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestsCovid_AspNetUsers_UserId",
                table: "TestsCovid");

            migrationBuilder.DropIndex(
                name: "IX_TestsCovid_UserId",
                table: "TestsCovid");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestsCovid");
        }
    }
}

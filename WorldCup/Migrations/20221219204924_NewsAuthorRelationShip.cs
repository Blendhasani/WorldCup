using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldCup.Migrations
{
    public partial class NewsAuthorRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Authors_AuthorId",
                table: "News",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Authors_AuthorId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_AuthorId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "News");
        }
    }
}

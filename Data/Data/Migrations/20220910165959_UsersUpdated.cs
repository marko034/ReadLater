using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UsersUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Bookmark",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_CreatedByUserId",
                table: "Bookmark",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_AspNetUsers_CreatedByUserId",
                table: "Bookmark",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_AspNetUsers_CreatedByUserId",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_CreatedByUserId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Bookmark");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    public partial class UsernameinCommentmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubComments_MainComments_MainCommentId",
                table: "SubComments");

            migrationBuilder.RenameColumn(
                name: "MainCommentId",
                table: "SubComments",
                newName: "MainCommentID");

            migrationBuilder.RenameIndex(
                name: "IX_SubComments_MainCommentId",
                table: "SubComments",
                newName: "IX_SubComments_MainCommentID");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SubComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "MainComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SubComments_MainComments_MainCommentID",
                table: "SubComments",
                column: "MainCommentID",
                principalTable: "MainComments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubComments_MainComments_MainCommentID",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "MainComments");

            migrationBuilder.RenameColumn(
                name: "MainCommentID",
                table: "SubComments",
                newName: "MainCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_SubComments_MainCommentID",
                table: "SubComments",
                newName: "IX_SubComments_MainCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubComments_MainComments_MainCommentId",
                table: "SubComments",
                column: "MainCommentId",
                principalTable: "MainComments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

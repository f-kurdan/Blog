using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    public partial class CommentSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.CreateTable(
            //        name: "MainComments",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            PostID = table.Column<int>(type: "int", nullable: true),
            //            Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Created = table.Column<DateTime>(type: "datetime2", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_MainComments", x => x.ID);
            //            table.ForeignKey(
            //                name: "FK_MainComments_Posts_PostID",
            //                column: x => x.PostID,
            //                principalTable: "Posts",
            //                principalColumn: "ID");
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "SubComments",
            //        columns: table => new
            //        {
            //            ID = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            MainCommentId = table.Column<int>(type: "int", nullable: false),
            //            Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //            Created = table.Column<DateTime>(type: "datetime2", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_SubComments", x => x.ID);
            //            table.ForeignKey(
            //                name: "FK_SubComments_MainComments_MainCommentId",
            //                column: x => x.MainCommentId,
            //                principalTable: "MainComments",
            //                principalColumn: "ID",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_MainComments_PostID",
            //        table: "MainComments",
            //        column: "PostID");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SubComments_MainCommentId",
            //        table: "SubComments",
            //        column: "MainCommentId");
            //}

            //protected override void Down(MigrationBuilder migrationBuilder)
            //{
            //    migrationBuilder.DropTable(
            //        name: "SubComments");

            //    migrationBuilder.DropTable(
            //        name: "MainComments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace UpYourChannel.Data.Migrations
{
    public partial class kk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedVideos_AspNetUsers_UserId1",
                table: "RequestedVideos");

            migrationBuilder.DropIndex(
                name: "IX_RequestedVideos_UserId1",
                table: "RequestedVideos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RequestedVideos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RequestedVideos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedVideos_UserId",
                table: "RequestedVideos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedVideos_AspNetUsers_UserId",
                table: "RequestedVideos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedVideos_AspNetUsers_UserId",
                table: "RequestedVideos");

            migrationBuilder.DropIndex(
                name: "IX_RequestedVideos_UserId",
                table: "RequestedVideos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RequestedVideos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "RequestedVideos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestedVideos_UserId1",
                table: "RequestedVideos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedVideos_AspNetUsers_UserId1",
                table: "RequestedVideos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

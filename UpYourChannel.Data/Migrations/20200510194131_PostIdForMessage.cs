using Microsoft.EntityFrameworkCore.Migrations;

namespace UpYourChannel.Data.Migrations
{
    public partial class PostIdForMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForPost",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "IsForPost",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

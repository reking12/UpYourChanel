using Microsoft.EntityFrameworkCore.Migrations;

namespace UpYourChannel.Data.Migrations
{
    public partial class MessageIsForPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForPost",
                table: "Messages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForPost",
                table: "Messages");
        }
    }
}

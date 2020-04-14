using Microsoft.EntityFrameworkCore.Migrations;

namespace UpYourChannel.Data.Migrations
{
    public partial class AddIsanswerInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnswer",
                table: "Comments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnswer",
                table: "Comments");
        }
    }
}

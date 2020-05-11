using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfModel.Migrations
{
    public partial class Removed_SexType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }
    }
}

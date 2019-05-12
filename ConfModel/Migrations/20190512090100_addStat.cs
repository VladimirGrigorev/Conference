using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfModel.Migrations
{
    public partial class addStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatus",
                table: "Applications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "Applications");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Conference.Migrations
{
    public partial class AdminOfConf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ThemeSectionName",
                table: "ThemeSection",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "AdminOfConference",
                columns: table => new
                {
                    AdminOfConferenceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<int>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminOfConference", x => x.AdminOfConferenceId);
                    table.ForeignKey(
                        name: "FK_AdminOfConference_Conference_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conference",
                        principalColumn: "ConferenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminOfConference_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminOfConference_ConferenceId",
                table: "AdminOfConference",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOfConference_UserId",
                table: "AdminOfConference",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminOfConference");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeSectionName",
                table: "ThemeSection",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

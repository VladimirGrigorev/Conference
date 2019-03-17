using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conference.Migrations
{
    public partial class ThemeSectionConference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConferenceId",
                table: "Section",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SectionName",
                table: "Section",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThemeSectionId",
                table: "Section",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ThemeConference",
                columns: table => new
                {
                    ThemeConferenceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ThemeConferenceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeConference", x => x.ThemeConferenceId);
                });

            migrationBuilder.CreateTable(
                name: "ThemeSection",
                columns: table => new
                {
                    ThemeSectionId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ThemeSectionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeSection", x => x.ThemeSectionId);
                });

            migrationBuilder.CreateTable(
                name: "Conference",
                columns: table => new
                {
                    ConferenceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ConferenceName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateTimeStartConference = table.Column<DateTime>(nullable: false),
                    DateTimeFinishConference = table.Column<DateTime>(nullable: false),
                    ThemeConferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conference", x => x.ConferenceId);
                    table.ForeignKey(
                        name: "FK_Conference_ThemeConference_ThemeConferenceId",
                        column: x => x.ThemeConferenceId,
                        principalTable: "ThemeConference",
                        principalColumn: "ThemeConferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Section_ConferenceId",
                table: "Section",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_ThemeSectionId",
                table: "Section",
                column: "ThemeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOfConference_ConferenceId",
                table: "AdminOfConference",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOfConference_UserId",
                table: "AdminOfConference",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conference_ThemeConferenceId",
                table: "Conference",
                column: "ThemeConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Conference_ConferenceId",
                table: "Section",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "ConferenceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_ThemeSection_ThemeSectionId",
                table: "Section",
                column: "ThemeSectionId",
                principalTable: "ThemeSection",
                principalColumn: "ThemeSectionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Conference_ConferenceId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_ThemeSection_ThemeSectionId",
                table: "Section");

            migrationBuilder.DropTable(
                name: "AdminOfConference");

            migrationBuilder.DropTable(
                name: "ThemeSection");

            migrationBuilder.DropTable(
                name: "Conference");

            migrationBuilder.DropTable(
                name: "ThemeConference");

            migrationBuilder.DropIndex(
                name: "IX_Section_ConferenceId",
                table: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Section_ThemeSectionId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "SectionName",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "ThemeSectionId",
                table: "Section");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conference.Migrations
{
    public partial class ConferenceSectionAdmin : Migration
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

            migrationBuilder.CreateTable(
                name: "Conference",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ConferenceName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateTimeStartConference = table.Column<DateTime>(nullable: false),
                    DateTimeFinishConference = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminOfConference",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<int>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminOfConference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminOfConference_Conference_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conference",
                        principalColumn: "Id",
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
                name: "IX_AdminOfConference_ConferenceId",
                table: "AdminOfConference",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOfConference_UserId",
                table: "AdminOfConference",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Conference_ConferenceId",
                table: "Section",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Conference_ConferenceId",
                table: "Section");

            migrationBuilder.DropTable(
                name: "AdminOfConference");

            migrationBuilder.DropTable(
                name: "Conference");

            migrationBuilder.DropIndex(
                name: "IX_Section_ConferenceId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "SectionName",
                table: "Section");
        }
    }
}

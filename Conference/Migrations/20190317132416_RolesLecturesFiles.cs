using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conference.Migrations
{
    public partial class RolesLecturesFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCloseChat",
                table: "Lectures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeLecture",
                table: "Lectures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeOpenChat",
                table: "Lectures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "Lectures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TopicLecture",
                table: "Lectures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Size = table.Column<double>(nullable: false),
                    Private = table.Column<short>(nullable: false),
                    LectureID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_Lectures_LectureID",
                        column: x => x.LectureID,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleInLecture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserID = table.Column<int>(nullable: false),
                    LectureID = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInLecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleInLecture_Lectures_LectureID",
                        column: x => x.LectureID,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleInLecture_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_SectionID",
                table: "Lectures",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_File_LectureID",
                table: "File",
                column: "LectureID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInLecture_LectureID",
                table: "RoleInLecture",
                column: "LectureID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInLecture_UserID",
                table: "RoleInLecture",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Section_SectionID",
                table: "Lectures",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Section_SectionID",
                table: "Lectures");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "RoleInLecture");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_SectionID",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "DateTimeCloseChat",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "DateTimeLecture",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "DateTimeOpenChat",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "TopicLecture",
                table: "Lectures");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfModel.Migrations
{
    public partial class addApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lectures_LectureId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Lectures_LectureId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Private",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "Messages",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_LectureId",
                table: "Messages",
                newName: "IX_Messages_ApplicationId");

            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "Files",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_LectureId",
                table: "Files",
                newName: "IX_Files_ApplicationId");

            migrationBuilder.AlterColumn<short>(
                name: "IsGlobalAdmin",
                table: "Users",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Files",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TempName",
                table: "Files",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Topic = table.Column<string>(maxLength: 300, nullable: true),
                    Authors = table.Column<string>(maxLength: 300, nullable: true),
                    Keywords = table.Column<string>(maxLength: 300, nullable: true),
                    Info = table.Column<string>(maxLength: 8000, nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_SectionId",
                table: "Applications",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Applications_ApplicationId",
                table: "Files",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Applications_ApplicationId",
                table: "Messages",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Applications_ApplicationId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Applications_ApplicationId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "TempName",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Messages",
                newName: "LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ApplicationId",
                table: "Messages",
                newName: "IX_Messages_LectureId");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Files",
                newName: "LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ApplicationId",
                table: "Files",
                newName: "IX_Files_LectureId");

            migrationBuilder.AlterColumn<short>(
                name: "IsGlobalAdmin",
                table: "Users",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AddColumn<short>(
                name: "Private",
                table: "Files",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Lectures_LectureId",
                table: "Files",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Lectures_LectureId",
                table: "Messages",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

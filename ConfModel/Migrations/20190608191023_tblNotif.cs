using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfModel.Migrations
{
    public partial class tblNotif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<int>(nullable: false),
                    ApplicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationNotifications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileNotifications_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageNotifications_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationNotifications_ApplicationId",
                table: "ApplicationNotifications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationNotifications_UserId",
                table: "ApplicationNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FileNotifications_FileId",
                table: "FileNotifications",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileNotifications_UserId",
                table: "FileNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageNotifications_MessageId",
                table: "MessageNotifications",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageNotifications_UserId",
                table: "MessageNotifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationNotifications");

            migrationBuilder.DropTable(
                name: "FileNotifications");

            migrationBuilder.DropTable(
                name: "MessageNotifications");
        }
    }
}

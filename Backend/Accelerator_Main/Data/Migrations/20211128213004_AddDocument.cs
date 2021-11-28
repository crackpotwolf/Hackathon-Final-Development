using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathName",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentInfos",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalName = table.Column<string>(type: "text", nullable: false),
                    PathName = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentInfos", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_DocumentInfos_Projects_ProjectGuid",
                        column: x => x.ProjectGuid,
                        principalTable: "Projects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileVersions",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentInfoGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    DateUpload = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OriginalName = table.Column<string>(type: "text", nullable: false),
                    PathName = table.Column<string>(type: "text", nullable: false),
                    PathNameParce = table.Column<string>(type: "text", nullable: false),
                    ParceStatus = table.Column<int>(type: "integer", nullable: false),
                    IndexStatus = table.Column<int>(type: "integer", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileVersions", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_FileVersions_DocumentInfos_DocumentInfoGuid",
                        column: x => x.DocumentInfoGuid,
                        principalTable: "DocumentInfos",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentInfos_ProjectGuid",
                table: "DocumentInfos",
                column: "ProjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FileVersions_DocumentInfoGuid",
                table: "FileVersions",
                column: "DocumentInfoGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileVersions");

            migrationBuilder.DropTable(
                name: "DocumentInfos");

            migrationBuilder.DropColumn(
                name: "PathName",
                table: "Projects");
        }
    }
}

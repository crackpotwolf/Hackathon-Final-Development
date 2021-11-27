using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CHUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    People = table.Column<int>(type: "integer", nullable: false),
                    Competence = table.Column<string>(type: "text", nullable: false),
                    University = table.Column<string>(type: "text", nullable: false),
                    Inn = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "FullProjects",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantName = table.Column<string>(type: "text", nullable: false),
                    ApplicantLastName = table.Column<string>(type: "text", nullable: false),
                    ApplicantEmail = table.Column<string>(type: "text", nullable: false),
                    ApplicantRole = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    CompanyField = table.Column<string>(type: "text", nullable: false),
                    CompanyStage = table.Column<string>(type: "text", nullable: false),
                    CompanyPeople = table.Column<int>(type: "integer", nullable: false),
                    CompanyCompetence = table.Column<string>(type: "text", nullable: false),
                    CompanyUniversity = table.Column<string>(type: "text", nullable: false),
                    CompanyInn = table.Column<string>(type: "text", nullable: false),
                    CompanyCountry = table.Column<string>(type: "text", nullable: false),
                    CompanyCity = table.Column<string>(type: "text", nullable: false),
                    CompanyWebsite = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    SaleStage = table.Column<string>(type: "text", nullable: false),
                    ImplementedPilotsCount = table.Column<int>(type: "integer", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    Descripption = table.Column<string>(type: "text", nullable: false),
                    ValueProposition = table.Column<string>(type: "text", nullable: false),
                    PilotCharacteristics = table.Column<string>(type: "text", nullable: false),
                    Budget = table.Column<string>(type: "text", nullable: false),
                    Competitors = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    Investment = table.Column<bool>(type: "boolean", nullable: false),
                    Expertise = table.Column<bool>(type: "boolean", nullable: false),
                    ExpertiseReport = table.Column<string>(type: "text", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: false),
                    Subfield = table.Column<string>(type: "text", nullable: false),
                    Technology = table.Column<List<string>>(type: "text[]", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullProjects", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Subfields",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subfields", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Subfields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    SaleStage = table.Column<string>(type: "text", nullable: false),
                    ImplementedPilotsCount = table.Column<int>(type: "integer", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    Descripption = table.Column<string>(type: "text", nullable: false),
                    ValueProposition = table.Column<string>(type: "text", nullable: false),
                    PilotCharacteristics = table.Column<string>(type: "text", nullable: false),
                    Budget = table.Column<string>(type: "text", nullable: false),
                    Competitors = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: false),
                    Investment = table.Column<bool>(type: "boolean", nullable: false),
                    Expertise = table.Column<bool>(type: "boolean", nullable: false),
                    ExpertiseReport = table.Column<string>(type: "text", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubfieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Projects_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Subfields_SubfieldId",
                        column: x => x.SubfieldId,
                        principalTable: "Subfields",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechnologies",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnologyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechnologies", x => new { x.ProjectId, x.TechnologyId });
                    table.ForeignKey(
                        name: "FK_ProjectTechnologies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechnologies_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ApplicantId",
                table: "Projects",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SubfieldId",
                table: "Projects",
                column: "SubfieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechnologies_TechnologyId",
                table: "ProjectTechnologies",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Subfields_FieldId",
                table: "Subfields",
                column: "FieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullProjects");

            migrationBuilder.DropTable(
                name: "ProjectTechnologies");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Subfields");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

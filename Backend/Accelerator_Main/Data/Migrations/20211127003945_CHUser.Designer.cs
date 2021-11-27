﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AcceleratorContext))]
    [Migration("20211127003945_CHUser")]
    partial class CHUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.DB.Account.Role", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSecret")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Models.DB.Account.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("EmailNormalized")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecurityStampEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Models.DB.Account.UserRoles", b =>
                {
                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleGuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.HasKey("UserGuid", "RoleGuid");

                    b.HasIndex("RoleGuid");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Applicant", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Company", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Competence")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("People")
                        .HasColumnType("integer");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Field", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Data.Models.DB.Project.FullProject", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApplicantEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ApplicantLastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ApplicantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ApplicantRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Budget")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyCity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyCompetence")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyCountry")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyField")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyInn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CompanyPeople")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyStage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyUniversity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyWebsite")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Competitors")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descripption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Expertise")
                        .HasColumnType("boolean");

                    b.Property<string>("ExpertiseReport")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ImplementedPilotsCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Investment")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PilotCharacteristics")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SaleStage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subfield")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Technology")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("ValueProposition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("FullProjects");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Project", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uuid");

                    b.Property<string>("Budget")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Competitors")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descripption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Expertise")
                        .HasColumnType("boolean");

                    b.Property<string>("ExpertiseReport")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ImplementedPilotsCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Investment")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PilotCharacteristics")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SaleStage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SubfieldId")
                        .HasColumnType("uuid");

                    b.Property<string>("ValueProposition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SubfieldId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Data.Models.DB.Project.ProjectTechnologies", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TechnologyId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("ProjectTechnologies");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Subfield", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("FieldId");

                    b.ToTable("Subfields");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Technology", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("Data.Models.DB.Account.UserRoles", b =>
                {
                    b.HasOne("Data.Models.DB.Account.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.DB.Account.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Project", b =>
                {
                    b.HasOne("Data.Models.DB.Project.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.DB.Project.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.DB.Project.Subfield", "Subfield")
                        .WithMany()
                        .HasForeignKey("SubfieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Company");

                    b.Navigation("Subfield");
                });

            modelBuilder.Entity("Data.Models.DB.Project.ProjectTechnologies", b =>
                {
                    b.HasOne("Data.Models.DB.Project.Project", "Project")
                        .WithMany("Technologies")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.DB.Project.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Subfield", b =>
                {
                    b.HasOne("Data.Models.DB.Project.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");
                });

            modelBuilder.Entity("Data.Models.DB.Account.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Data.Models.DB.Account.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Data.Models.DB.Project.Project", b =>
                {
                    b.Navigation("Technologies");
                });
#pragma warning restore 612, 618
        }
    }
}

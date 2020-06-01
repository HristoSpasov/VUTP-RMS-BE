﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RMS.Data;

namespace RMS.Data.Migrations
{
    [DbContext(typeof(RMS_Db_Context))]
    [Migration("20200527051929_Initial-Entity-Configuration")]
    partial class InitialEntityConfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RMS.Data.Entities.ChangeLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("KeyValues");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.Property<string>("TableName");

                    b.HasKey("Id");

                    b.ToTable("ChangeLogs");
                });

            modelBuilder.Entity("RMS.Data.Entities.Discipline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("RMS.Data.Entities.DisciplineEvent", b =>
                {
                    b.Property<Guid>("EventId");

                    b.Property<Guid>("DisciplineId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.HasKey("EventId", "DisciplineId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("DisciplineId");

                    b.ToTable("DisciplinesEvents");
                });

            modelBuilder.Entity("RMS.Data.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("RMS.Data.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RMS.Data.Entities.RoomEvent", b =>
                {
                    b.Property<Guid>("EventId");

                    b.Property<Guid>("RoomId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.HasKey("EventId", "RoomId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomsEvents");
                });

            modelBuilder.Entity("RMS.Data.Entities.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("Grade");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("RMS.Data.Entities.SpecialtyDiscipline", b =>
                {
                    b.Property<Guid>("SpecialtyId");

                    b.Property<Guid>("DisciplineId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.HasKey("SpecialtyId", "DisciplineId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("DisciplineId");

                    b.ToTable("SpecialtiesDisciplines");
                });

            modelBuilder.Entity("RMS.Data.Entities.SpecialtyEvent", b =>
                {
                    b.Property<Guid>("EventId");

                    b.Property<Guid>("SpecialtyId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.HasKey("EventId", "SpecialtyId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("SpecialtiesEvents");
                });

            modelBuilder.Entity("RMS.Data.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcademicTitle")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("LastUpdated");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("RMS.Data.Entities.TeacherEvent", b =>
                {
                    b.Property<Guid>("EventId");

                    b.Property<Guid>("TeacherId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.HasKey("EventId", "TeacherId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeachersEvents");
                });

            modelBuilder.Entity("RMS.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<long?>("FacebookId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PictureUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RMS.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RMS.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RMS.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RMS.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RMS.Data.Entities.DisciplineEvent", b =>
                {
                    b.HasOne("RMS.Data.Entities.Discipline", "Discipline")
                        .WithMany("DisciplineEvents")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RMS.Data.Entities.Event", "Event")
                        .WithMany("EventDisciplines")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("RMS.Data.Entities.RoomEvent", b =>
                {
                    b.HasOne("RMS.Data.Entities.Event", "Event")
                        .WithMany("EventRooms")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RMS.Data.Entities.Room", "Room")
                        .WithMany("RoomEvents")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("RMS.Data.Entities.SpecialtyDiscipline", b =>
                {
                    b.HasOne("RMS.Data.Entities.Discipline", "Discipline")
                        .WithMany("DisciplineSpecialties")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RMS.Data.Entities.Specialty", "Specialty")
                        .WithMany("SpecialtyDisciplines")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("RMS.Data.Entities.SpecialtyEvent", b =>
                {
                    b.HasOne("RMS.Data.Entities.Event", "Event")
                        .WithMany("EventSpecialties")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RMS.Data.Entities.Specialty", "Specialty")
                        .WithMany("SpecialtyEvents")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("RMS.Data.Entities.TeacherEvent", b =>
                {
                    b.HasOne("RMS.Data.Entities.Event", "Event")
                        .WithMany("EventTeachers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RMS.Data.Entities.Teacher", "Teacher")
                        .WithMany("TeacherEvents")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

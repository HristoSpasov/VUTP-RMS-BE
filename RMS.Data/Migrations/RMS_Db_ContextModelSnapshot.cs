﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RMS.Data;

namespace RMS.Data.Migrations
{
    [DbContext(typeof(RMS_Db_Context))]
    partial class RMS_Db_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("RMS.Data.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("DisciplineId");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastEditedBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

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

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.HasIndex("RoomId");

                    b.ToTable("RoomEvent");
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

                    b.ToTable("Specialities");
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

                    b.ToTable("SpecialtyDiscipline");
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

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherEvent");
                });

            modelBuilder.Entity("RMS.Data.Entities.Event", b =>
                {
                    b.HasOne("RMS.Data.Entities.Discipline", "Discipline")
                        .WithMany("Events")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RMS.Data.Entities.RoomEvent", b =>
                {
                    b.HasOne("RMS.Data.Entities.Event", "Event")
                        .WithOne("RoomEvent")
                        .HasForeignKey("RMS.Data.Entities.RoomEvent", "EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RMS.Data.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RMS.Data.Entities.SpecialtyDiscipline", b =>
                {
                    b.HasOne("RMS.Data.Entities.Discipline", "Discipline")
                        .WithMany("DisciplineSpecialties")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RMS.Data.Entities.Specialty", "Specialty")
                        .WithMany("SpecialtiesDiscipline")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RMS.Data.Entities.TeacherEvent", b =>
                {
                    b.HasOne("RMS.Data.Entities.Event", "Event")
                        .WithOne("TeacherEvent")
                        .HasForeignKey("RMS.Data.Entities.TeacherEvent", "EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RMS.Data.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

namespace RMS.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeDatabaseSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtyDiscipline_Disciplines_DisciplineId",
                table: "SpecialtyDiscipline");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtyDiscipline_Specialities_SpecialtyId",
                table: "SpecialtyDiscipline");

            migrationBuilder.DropTable(
                name: "RoomEvent");

            migrationBuilder.DropTable(
                name: "TeacherEvent");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SpecialtyDiscipline_Id",
                table: "SpecialtyDiscipline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialtyDiscipline",
                table: "SpecialtyDiscipline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities");

            migrationBuilder.RenameTable(
                name: "SpecialtyDiscipline",
                newName: "SpecialtiesDisciplines");

            migrationBuilder.RenameTable(
                name: "Specialities",
                newName: "Specialties");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialtyDiscipline_DisciplineId",
                table: "SpecialtiesDisciplines",
                newName: "IX_SpecialtiesDisciplines_DisciplineId");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Events",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Events",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SpecialtiesDisciplines_Id",
                table: "SpecialtiesDisciplines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialtiesDisciplines",
                table: "SpecialtiesDisciplines",
                columns: new[] { "SpecialtyId", "DisciplineId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SpecialtiesEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    LastEditedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    SpecialtyId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtiesEvents", x => new { x.EventId, x.SpecialtyId });
                    table.UniqueConstraint("AK_SpecialtiesEvents_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialtiesEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialtiesEvents_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_RoomId",
                table: "Events",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TeacherId",
                table: "Events",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtiesEvents_SpecialtyId",
                table: "SpecialtiesEvents",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Rooms_RoomId",
                table: "Events",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Teachers_TeacherId",
                table: "Events",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtiesDisciplines_Disciplines_DisciplineId",
                table: "SpecialtiesDisciplines",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtiesDisciplines_Specialties_SpecialtyId",
                table: "SpecialtiesDisciplines",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Rooms_RoomId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Teachers_TeacherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtiesDisciplines_Disciplines_DisciplineId",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtiesDisciplines_Specialties_SpecialtyId",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropTable(
                name: "SpecialtiesEvents");

            migrationBuilder.DropIndex(
                name: "IX_Events_RoomId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_TeacherId",
                table: "Events");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SpecialtiesDisciplines_Id",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialtiesDisciplines",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "SpecialtiesDisciplines",
                newName: "SpecialtyDiscipline");

            migrationBuilder.RenameTable(
                name: "Specialties",
                newName: "Specialities");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialtiesDisciplines_DisciplineId",
                table: "SpecialtyDiscipline",
                newName: "IX_SpecialtyDiscipline_DisciplineId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SpecialtyDiscipline_Id",
                table: "SpecialtyDiscipline",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialtyDiscipline",
                table: "SpecialtyDiscipline",
                columns: new[] { "SpecialtyId", "DisciplineId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialities",
                table: "Specialities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoomEvent",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastEditedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEvent", x => new { x.EventId, x.RoomId });
                    table.UniqueConstraint("AK_RoomEvent_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomEvent_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEvent",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastEditedBy = table.Column<string>(maxLength: 100, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEvent", x => new { x.EventId, x.TeacherId });
                    table.UniqueConstraint("AK_TeacherEvent_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherEvent_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomEvent_EventId",
                table: "RoomEvent",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomEvent_RoomId",
                table: "RoomEvent",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEvent_EventId",
                table: "TeacherEvent",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEvent_TeacherId",
                table: "TeacherEvent",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtyDiscipline_Disciplines_DisciplineId",
                table: "SpecialtyDiscipline",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtyDiscipline_Specialities_SpecialtyId",
                table: "SpecialtyDiscipline",
                column: "SpecialtyId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

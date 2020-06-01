namespace RMS.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtiesDisciplines_Specialties_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtiesEvents_Specialties_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesEvents");

            migrationBuilder.DropIndex(
                name: "IX_SpecialtiesEvents_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesEvents");

            migrationBuilder.DropIndex(
                name: "IX_SpecialtiesDisciplines_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Specialties_Id",
                table: "Specialties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_Number",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_Name",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "SpecialtyGrade",
                table: "SpecialtiesEvents");

            migrationBuilder.DropColumn(
                name: "SpecialtyName",
                table: "SpecialtiesEvents");

            migrationBuilder.DropColumn(
                name: "SpecialtyGrade",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropColumn(
                name: "SpecialtyName",
                table: "SpecialtiesDisciplines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtiesEvents_SpecialtyId",
                table: "SpecialtiesEvents",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtiesDisciplines_Specialties_SpecialtyId",
                table: "SpecialtiesDisciplines",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtiesEvents_Specialties_SpecialtyId",
                table: "SpecialtiesEvents",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtiesDisciplines_Specialties_SpecialtyId",
                table: "SpecialtiesDisciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtiesEvents_Specialties_SpecialtyId",
                table: "SpecialtiesEvents");

            migrationBuilder.DropIndex(
                name: "IX_SpecialtiesEvents_SpecialtyId",
                table: "SpecialtiesEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyGrade",
                table: "SpecialtiesEvents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialtyName",
                table: "SpecialtiesEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyGrade",
                table: "SpecialtiesDisciplines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialtyName",
                table: "SpecialtiesDisciplines",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Specialties_Id",
                table: "Specialties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                columns: new[] { "Name", "Grade" });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtiesEvents_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesEvents",
                columns: new[] { "SpecialtyName", "SpecialtyGrade" });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtiesDisciplines_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesDisciplines",
                columns: new[] { "SpecialtyName", "SpecialtyGrade" });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Name",
                table: "Disciplines",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtiesDisciplines_Specialties_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesDisciplines",
                columns: new[] { "SpecialtyName", "SpecialtyGrade" },
                principalTable: "Specialties",
                principalColumns: new[] { "Name", "Grade" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtiesEvents_Specialties_SpecialtyName_SpecialtyGrade",
                table: "SpecialtiesEvents",
                columns: new[] { "SpecialtyName", "SpecialtyGrade" },
                principalTable: "Specialties",
                principalColumns: new[] { "Name", "Grade" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}

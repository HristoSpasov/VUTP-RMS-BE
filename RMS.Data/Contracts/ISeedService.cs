namespace RMS.Data.Contracts
{
    public interface ISeedService
    {
        void SeedAll();

        ISeedService SeedSpecialty();

        ISeedService SeedDisciplines();

        ISeedService SeedRooms();

        ISeedService SeedTeachers();

        ISeedService SeedSpecialtyDiscipline();

        ISeedService SeedEvents();

        ISeedService SeedRoomEvent();

        ISeedService SeedTeacherEvent();
    }
}
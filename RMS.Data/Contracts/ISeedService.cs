namespace RMS.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ISeedService
    {
        void SeedAll();

        ISeedService SeedSpecialty();

        ISeedService SeedDisciplines();

        ISeedService SeedRooms();

        ISeedService SeedTeachers();

        ISeedService SeedSpecialtyDiscipline();

        ISeedService SeedEvents();

        Task<ISeedService> SeedUsersAndRoles();
    }
}
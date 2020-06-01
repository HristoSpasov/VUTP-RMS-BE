namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class DisciplineRepository : GenericRepository<Discipline>, IDisciplineRepository
    {
        public DisciplineRepository(RMS_Db_Context context)
            : base(context)
        {
        }
    }
}

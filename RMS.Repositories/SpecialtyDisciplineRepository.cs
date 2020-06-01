namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class SpecialtyDisciplineRepository : GenericRepository<SpecialtyDiscipline>, ISpecialtyDisciplineRepository
    {
        public SpecialtyDisciplineRepository(RMS_Db_Context context)
            : base(context)
        {
        }
    }
}

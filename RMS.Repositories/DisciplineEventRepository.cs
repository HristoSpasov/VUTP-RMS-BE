namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class DisciplineEventRepository : GenericRepository<DisciplineEvent>, IDisciplineEventRepository
    {
        public DisciplineEventRepository(RMS_Db_Context context) 
            : base(context)
        {
        }
    }
}

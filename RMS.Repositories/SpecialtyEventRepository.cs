namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class SpecialtyEventRepository : GenericRepository<SpecialtyEvent>, ISpecialtyEventRepository
    {
        public SpecialtyEventRepository(RMS_Db_Context context)
           : base(context)
        {

        }
    }
}

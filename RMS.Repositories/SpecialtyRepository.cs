namespace RMS.Repositories
{
    using Contracts;
    using Data;
    using Data.Entities;

    public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(RMS_Db_Context context) : base(context)
        {
        }
    }
}
namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(RMS_Db_Context context) 
            : base(context)
        {
        }
    }
}

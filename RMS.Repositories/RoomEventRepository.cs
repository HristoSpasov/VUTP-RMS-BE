namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class RoomEventRepository : GenericRepository<RoomEvent>, IRoomEventRepository
    {
        public RoomEventRepository(RMS_Db_Context context)
            : base (context)
        {
        }
    }
}

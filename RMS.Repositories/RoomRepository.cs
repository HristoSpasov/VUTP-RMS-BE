namespace RMS.Repositories
{
    using Contracts;
    using Data;
    using Data.Entities;

    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(RMS_Db_Context context) : base(context)
        {
        }
    }
}

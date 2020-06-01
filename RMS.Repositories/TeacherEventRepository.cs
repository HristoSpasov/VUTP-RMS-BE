namespace RMS.Repositories
{
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;

    public class TeacherEventRepository : GenericRepository<TeacherEvent>, ITeacherEventRepository
    {
        public TeacherEventRepository(RMS_Db_Context context) 
            : base(context)
        {
        }
    }
}

namespace RMS.Repositories
{
    using Contracts;
    using Data;
    using Data.Entities;

    /// <summary>
    /// Teacher entity repository implementation.
    /// </summary>
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherRepository"/> class.
        /// </summary>
        /// <param name="context">RMS database context.</param>
        public TeacherRepository(RMS_Db_Context context)
            : base(context)
        {
        }
    }
}
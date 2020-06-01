namespace RMS.Repositories.Contracts
{
    using RMS.Data.Entities;
    using System.Threading.Tasks;

    public interface IAccountRepository
    {
       /// <summary>
       /// Add user.
       /// </summary>
       /// <param name="entity"></param>
        void Add(User entity);

        /// <summary>
        /// Add user async.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(User entity);

        /// <summary>
        /// Save changes.
        /// </summary>
        void Save();

        /// <summary>
        /// Save async.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}

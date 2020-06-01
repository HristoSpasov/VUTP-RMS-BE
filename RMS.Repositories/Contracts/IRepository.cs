namespace RMS.Repositories.Contracts
{
    using Data.Entities;
    using Microsoft.EntityFrameworkCore.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///  Contract for generic repository
    /// </summary>
    /// <typeparam name="T">Entity object</typeparam>
    public interface IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Find first record matching certain criteria allowing include, sort and filter.
        /// </summary>
        /// <param name="include">Include properties func.</param>
        /// <param name="predicate">Filter condition.</param>
        /// <param name="orderBy">Order condition.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Query with including data from other entities.</returns>
        /// <returns>First record matching certain criteria.</returns>
        T Find(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Find first record matching certain criteria allowing include, sort and filter async.
        /// </summary>
        /// <param name="include">Include properties func.</param>
        /// <param name="predicate">Filter condition.</param>
        /// <param name="orderBy">Order condition.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Query with including data from other entities.</returns>
        /// <returns>First record matching certain criteria.</returns>
        Task<T> FindAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Collection, containing all data including data from other entities. Allowing include, sort and filter collection.
        /// </summary>
        /// <param name="include">Include properties func.</param>
        /// <param name="predicate">Filter condition.</param>
        /// <param name="orderBy">Order condition.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Collection of T, including data from other entities.</returns>
        ICollection<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Collection, containing all data including data from other entities async. Allowing include, sort and filter collection.
        /// </summary>
        /// <param name="include">Include properties func.</param>
        /// <param name="predicate">Filter condition.</param>
        /// <param name="orderBy">Order condition.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Query with including data from other entities.</returns>
        Task<ICollection<T>> FindAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Get single record by Id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Single record with id.</returns>
        T Get(object id, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Get single record by Id async.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Single record with id.</returns>
        Task<T> GetAsync(object id, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Get single record by Id allowing to include data from other entities.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="include">Include properties func.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Single record with id.</returns>
        T GetIncluding(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Get single record by Id allowing to include data from other entities async.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <param name="include">Include properties func.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Single record with id.</returns>
        Task<T> GetIncludingAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Get all records query.
        /// </summary>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <returns>Query for retrieving of all records.</returns>
        ICollection<T> GetAll(bool enableTracking = true);

        /// <summary>
        /// Get all records async.
        /// </summary>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <returns>Collection of all records.</returns>
        Task<ICollection<T>> GetAllAsync(bool enableTracking = true);

        /// <summary>
        /// Get all records allowing to include data from other entities.
        /// </summary>
        /// <param name="include">Include properties func.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Query for retrieving of all records.</returns>
        ICollection<T> GetAllIncluding(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Get all records allowing to include data from other entities async.
        /// </summary>
        /// <param name="include">Include properties func.</param>
        /// <param name="enableTracking">Enable tracking. Default is true.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Collection of all records.</returns>
        Task<ICollection<T>> GetAllIncludingAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false);

        /// <summary>
        /// Save changes.
        /// </summary>
        void Save();

        /// <summary>
        /// Save changes async.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveAsync();

        /// <summary>
        /// Adding T entity to DB.
        /// </summary>
        /// <param name="entity">Database entity.</param>
        void Add(T entity);

        /// <summary>
        /// Adding T entity to DB async.
        /// </summary>
        /// <param name="entity">Database entity.</param>
        Task  AddAsync(T entity);

        /// <summary>
        /// Method Responsible for deleting given entity.
        /// </summary>
        /// <param name="entity">Database entity.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(T entity);

        /// <summary>
        /// Method Responsible for deleting from DB with specified ID
        /// </summary>
        /// <param name="id">Id of entity in database</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Delete(Guid id);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Update entity async.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Dispose repository.
        /// </summary>
        void Dispose();
    }
}
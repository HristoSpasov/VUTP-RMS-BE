using System;
using System.Collections.Generic;
using System.Text;

namespace RMS.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using RMS.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    /// <summary>
    /// Repository inherited from IRepository
    /// </summary>
    /// <typeparam name="T">T Entity</typeparam>
    public abstract class GenericRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Interactive map context field.
        /// </summary>
        private readonly RMS_Db_Context context;

        /// <summary>
        /// Disposed private field.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">Interactive map context parameter.</param>
        protected GenericRepository(RMS_Db_Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets Interactive map context protected property.
        /// </summary>
        protected RMS_Db_Context Context => this.context;

        /// <summary>
        /// Gets set of type T.
        /// </summary>
        protected DbSet<T> Set => this.context.Set<T>();

        /// <inheritdoc/>
        public T Find(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return this.GenerateQuery(include, predicate, orderBy, enableTracking, ignoreQueryFilter).FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task<T> FindAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return await this.GenerateQuery(include, predicate, orderBy, enableTracking, ignoreQueryFilter).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public ICollection<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return this.GenerateQuery(include, predicate, orderBy, enableTracking, ignoreQueryFilter).ToList();
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> FindAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return await this.GenerateQuery(include, predicate, orderBy, enableTracking, ignoreQueryFilter).ToListAsync();
        }

        /// <inheritdoc/>
        public T Get(object id, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            var entity = default(T);

            if (ignoreQueryFilter)
            {
                entity = this.context.Set<T>().IgnoreQueryFilters().FirstOrDefault(i => i.Id.Equals(id));
            }
            else
            {
                entity = this.context.Set<T>().Find(id);
            }

            if (!enableTracking)
            {
                this.Detach(entity);
            }

            return entity;
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync(object id, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            var entity = default(T);

            if (ignoreQueryFilter)
            {
                entity = await this.context.Set<T>().IgnoreQueryFilters().FirstOrDefaultAsync(i => i.Id.Equals(id));
            }
            else
            {
                entity = await this.context.Set<T>().FindAsync(id);
            }

            if (!enableTracking)
            {
                this.Detach(entity);
            }

            return entity;
        }

        /// <inheritdoc/>
        public T GetIncluding(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return this.GenerateQuery(include, enableTracking: enableTracking, ignoreQueryFilter: ignoreQueryFilter).SingleOrDefault(i => i.Id == id);
        }

        /// <inheritdoc/>
        public Task<T> GetIncludingAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return this.GenerateQuery(include, enableTracking: enableTracking, ignoreQueryFilter: ignoreQueryFilter).SingleOrDefaultAsync(i => i.Id == id);
        }

        /// <inheritdoc/>
        public ICollection<T> GetAll(bool enableTracking = true)
        {
            var set = this.context.Set<T>();

            return !enableTracking ? set.AsNoTracking().ToList() : set.ToList();
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> GetAllAsync(bool enableTracking = true)
        {
            var set = this.context.Set<T>();

            if (!enableTracking)
            {
                return await set.AsNoTracking().ToListAsync();
            }

            return await set.ToListAsync();
        }

        /// <inheritdoc/>
        public ICollection<T> GetAllIncluding(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return this.GenerateQuery(include, enableTracking: enableTracking, ignoreQueryFilter: ignoreQueryFilter).ToList();
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> GetAllIncludingAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            return await this.GenerateQuery(include, enableTracking: enableTracking, ignoreQueryFilter: ignoreQueryFilter).ToListAsync();
        }

        /// <inheritdoc/>
        public void Save()
        {
            this.context.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        /// <inheritdoc/>
        public virtual Task Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public virtual Task Delete(Guid id)
        {
            var entity = this.Get(id, true);

            if (entity != null)
            {
                this.Delete(entity);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            if (entity == null)
            {
                return;
            }

            T exist = this.Get(entity.Id);

            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(entity);
            }
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                return;
            }

            T exist = await this.GetAsync(entity.Id);

            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(entity);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose repository.
        /// </summary>
        /// <param name="disposing">Boolean disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Set entity state to detached.
        /// </summary>
        /// <param name="entity">T entity.</param>
        private void Detach(T entity)
        {
            this.context.Entry<T>(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Generate qury according to include, sort and order conditions.
        /// </summary>
        /// <param name="include">Related entities to include.</param>
        /// <param name="predicate">Filter records predicate.</param>
        /// <param name="orderBy">Order by condition.</param>
        /// <param name="enableTracking">Enable or disable tracking. By default tracking is enabled.</param>
        /// <param name="ignoreQueryFilter">Ignore query filters.</param>
        /// <returns>Query, according to parameters.</returns>
        private IQueryable<T> GenerateQuery(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool enableTracking = true, bool ignoreQueryFilter = false)
        {
            IQueryable<T> query = this.context.Set<T>();

            if (!enableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (ignoreQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }
    }
}

namespace RMS.Repositories
{
    using Microsoft.EntityFrameworkCore.Query;
    using RMS.Data;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class AccountRepository : IAccountRepository
    {
        private readonly RMS_Db_Context context;

        public AccountRepository(RMS_Db_Context context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public void Add(User entity)
        {
            this.context.Users.Add(entity);
        }

        /// <inheritdoc/>
        public async Task AddAsync(User entity)
        {
            await this.context.Users.AddAsync(entity);
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
    }
}

namespace RMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class RMS_Db_Context : DbContext
    {
        /// <summary>
        /// Field containing instance of EntityConfiguration class.
        /// </summary>
        private readonly EntityConfiguration entityConfig;

        /// <summary>
        /// Field containing logger instance.
        /// </summary>
        private readonly ILogger<RMS_Db_Context> logger;

        public RMS_Db_Context()
            : this(new DbContextOptionsBuilder().UseSqlServer("Server=.;Database=InteractiveMapDb;Trusted_Connection=True;ConnectRetryCount=0").Options, new EntityConfiguration(), new Logger<RMS_Db_Context>(new LoggerFactory()))
        {
            // This connection string is used only when calling update-database manually. Otherwise the database initialization strategy is UpdateDatabaseToLatestVersion
            // and custom implementation is being called into IoC container when the DbContext is being registered.

            // NOTE: Seed method will not be called if update-database command is executed manually
        }

        public RMS_Db_Context(DbContextOptions options, EntityConfiguration entityConfig, ILogger<RMS_Db_Context> logger)
            : base(options)
        {
            this.entityConfig = entityConfig;
            this.logger = logger;
        }
    }
}
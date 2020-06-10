namespace RMS.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using RMS.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class RMS_Db_Context : IdentityDbContext<User>
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
            : this(new DbContextOptionsBuilder().UseSqlServer("Server=.;Database=VUTP-RMS-SQL;Trusted_Connection=True;ConnectRetryCount=0").Options, new EntityConfiguration(), new Logger<RMS_Db_Context>(new LoggerFactory()))
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

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<ChangeLog> ChangeLogs { get; set; }

        public DbSet<SpecialtyDiscipline> SpecialtiesDisciplines { get; set; }

        public DbSet<SpecialtyEvent> SpecialtiesEvents { get; set; }

        public DbSet<DisciplineEvent> DisciplinesEvents { get; set; }

        public DbSet<TeacherEvent> TeachersEvents { get; set; }

        public DbSet<RoomEvent> RoomsEvents { get; set; }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var auditEntries = this.OnBeforeSaveChanges();
                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                await this.OnAfterSaveChanges(auditEntries);

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"Failed to save changes async. Exception message: {ex.Message}");
                return default(int);
            }
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            try
            {
                var auditEntries = this.OnBeforeSaveChanges();
                var result = base.SaveChanges();
                this.OnAfterSaveChanges(auditEntries).Wait();

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"Failed to save changes. Exception message: {ex.Message}");
                return default(int);
            }
        }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {
                var auditEntries = this.OnBeforeSaveChanges();
                var result = base.SaveChanges(acceptAllChangesOnSuccess);
                this.OnAfterSaveChanges(auditEntries).Wait();

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"Failed to save changes. Exception message: {ex.Message}");
                return default(int);
            }
        }

        /// <summary>
        /// Configuring database relationships
        /// </summary>
        /// <param name="modelBuilder">
        /// Provides a simple API surface for configuring a <see cref="T:Microsoft.EntityFrameworkCore.Metadata.IMutableModel" />
        /// that defines the shape of you entities, the relationships between them, and how they map to the database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.entityConfig.Configure(modelBuilder);

            // Configuring query filters.
            var setGlobalQueryFilterMethod = typeof(RMS_Db_Context).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(m => m.IsGenericMethod && m.Name == nameof(this.SetGlobalQuery));
            Assembly.GetAssembly(typeof(BaseEntity))?.DefinedTypes.Where(t => t.BaseType == typeof(BaseEntity)).Select(t => t.AsType()).ToList().ForEach(
                type =>
                {
                    var method = setGlobalQueryFilterMethod.MakeGenericMethod(type);
                    method.Invoke(this, new object[] { modelBuilder });
                });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Method to add query filter for specified type.
        /// </summary>
        /// <typeparam name="T">T is a base entity type.</typeparam>
        /// <param name="builder">Model builder parameter.</param>
        private void SetGlobalQuery<T>(ModelBuilder builder)
            where T : BaseEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        /// <summary>
        /// Handler that extracts the entities that are being changed from the EF change tracker
        /// </summary>
        /// <returns>Collection of changed entities</returns>
        private ICollection<ChangelogEntry> OnBeforeSaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            var auditEntries = new List<ChangelogEntry>();

            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is ChangeLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new ChangelogEntry(entry)
                {
                    TableName = entry.Metadata.Relational().TableName
                };

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(e => !e.HasTemporaryProperties))
            {
                this.ChangeLogs.Add(auditEntry.ToChangeLog());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(e => e.HasTemporaryProperties).ToList();
        }

        /// <summary>
        /// On after save changes handler
        /// </summary>
        /// <param name="auditEntries">ChangedEntries</param>
        /// <returns>Task</returns>
        private Task OnAfterSaveChanges(ICollection<ChangelogEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                this.ChangeLogs.Add(auditEntry.ToChangeLog());
            }

            return this.SaveChangesAsync();
        }
    }
}
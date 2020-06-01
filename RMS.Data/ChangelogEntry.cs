namespace RMS.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Internal;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Change log entry behaviour.
    /// </summary>
    public class ChangelogEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangelogEntry"/> class.
        /// </summary>
        /// <param name="entry">Entity entry.</param>
        public ChangelogEntry(EntityEntry entry)
        {
            this.Entry = entry;
        }

        /// <summary>
        /// Gets entity entry.
        /// </summary>
        public EntityEntry Entry { get; }

        /// <summary>
        /// Gets or sets table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets key values.
        /// </summary>
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets old values.
        /// </summary>
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets new values.
        /// </summary>
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets temporary properties.
        /// </summary>
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        /// <summary>
        /// Gets a value indicating whether there are temporary properties.
        /// </summary>
        public bool HasTemporaryProperties => this.TemporaryProperties.Any();

        /// <summary>
        /// Creates new change log.
        /// </summary>
        /// <returns>Change log.</returns>
        public ChangeLog ToChangeLog()
        {
            var audit = new ChangeLog
            {
                TableName = this.TableName,
                DateTime = DateTime.UtcNow,
                KeyValues = JsonConvert.SerializeObject(this.KeyValues),
                OldValues = this.OldValues.Count == 0 ? null : JsonConvert.SerializeObject(this.OldValues),
                NewValues = this.NewValues.Count == 0 ? null : JsonConvert.SerializeObject(this.NewValues)
            };

            return audit;
        }
    }
}
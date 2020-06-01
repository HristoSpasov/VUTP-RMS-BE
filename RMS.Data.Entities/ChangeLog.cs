namespace RMS.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Db entity that track all changes in the database from the current application
    /// </summary>
    // TODO: Track who is making the change
    public class ChangeLog
    {
        /// <summary>
        /// Gets or sets the entity unique identifier. AUTO GENERATED
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the modified entity's table
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets when the entity was modified
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        ///  Gets or sets entity key values
        /// </summary>
        public string KeyValues { get; set; }

        /// <summary>
        ///  Gets or sets entity old values as JSON
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// Gets or sets entity new values as JSON
        /// </summary>
        public string NewValues { get; set; }
    }
}
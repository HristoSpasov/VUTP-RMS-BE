namespace RMS.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Base entity model.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        public BaseEntity()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.LastUpdated = this.LastUpdated ?? DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the entity's unique identifier. AUTO GENERATED
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity creation datetime. UTC DateTime.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted. Soft delete.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the entity deletion datetime. Nullable UTC DateTime.
        /// </summary>
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who last edited the entity.
        /// </summary>
        [MaxLength(100)]
        public string LastEditedBy { get; set; }

        /// <summary>
        /// Gets or sets the entity last updated datetime. Nullable UTC DateTime.
        /// </summary>
        public DateTime? LastUpdated { get; set; }
    }
}
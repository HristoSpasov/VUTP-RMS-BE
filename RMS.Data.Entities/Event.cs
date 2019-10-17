namespace RMS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event : BaseEntity
    {
        [Required]
        public Guid DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        [Required]
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public ICollection<SpecialtyEvent> SpecialtiesEvents { get; set; }
    }
}

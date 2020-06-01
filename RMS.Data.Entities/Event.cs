namespace RMS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event : BaseEntity
    {
        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        public ICollection<SpecialtyEvent> EventSpecialties { get; set; }

        public ICollection<RoomEvent> EventRooms { get; set; }

        public ICollection<DisciplineEvent> EventDisciplines { get; set; }

        public ICollection<TeacherEvent> EventTeachers { get; set; }
    }
}
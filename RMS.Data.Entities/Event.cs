namespace RMS.Data.Entities
{
    using System;

    public class Event : BaseEntity
    {
        public Guid DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public TeacherEvent TeacherEvent { get; set; }

        public RoomEvent RoomEvent { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}

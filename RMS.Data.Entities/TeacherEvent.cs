namespace RMS.Data.Entities
{
    using System;

    public class TeacherEvent : BaseEntity
    {
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public Guid EventId { get; set; }

        public Event Event { get; set; }
    }
}

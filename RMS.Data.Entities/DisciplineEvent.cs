namespace RMS.Data.Entities
{
    using System;

    public class DisciplineEvent : BaseEntity
    {
        public Guid DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public Guid EventId { get; set; }

        public Event Event { get; set; }
    }
}

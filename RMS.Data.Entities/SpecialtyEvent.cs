namespace RMS.Data.Entities
{
    using System;

    public class SpecialtyEvent : BaseEntity
    {
        public Guid SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }

        public Guid EventId { get; set; }

        public Event Event { get; set; }
    }
}

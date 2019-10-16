namespace RMS.Data.Entities
{
    using System;

    public class RoomEvent : BaseEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}

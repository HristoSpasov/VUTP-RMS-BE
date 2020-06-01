namespace RMS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RoomEvent : BaseEntity
    {
        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        public Guid EventId { get; set; }

        public Event Event { get; set; }
    }
}

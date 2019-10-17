namespace RMS.Data.Entities
{
    using System.Collections.Generic;

    public class Room : BaseEntity
    {
        public string Number { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}

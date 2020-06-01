namespace RMS.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Room : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength =1)]
        public string Number { get; set; }

        public ICollection<RoomEvent> RoomEvents { get; set; }
    }
}
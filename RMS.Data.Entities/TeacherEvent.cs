using System;
using System.Collections.Generic;
using System.Text;

namespace RMS.Data.Entities
{
    public class TeacherEvent : BaseEntity
    {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}

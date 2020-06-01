namespace RMS.API.Models.RequestModels
{
    using System;
    using System.Collections.Generic;

    public class BaseEventRequestModel
    {
        public Guid Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public ICollection<Guid> Rooms { get; set; }
        public ICollection<Guid> Teachers { get; set; }
        public ICollection<Guid> Disciplines { get; set; }
        public ICollection<Guid> Specialties { get; set; }
    }
}

namespace RMS.API.Models.RequestModels
{
    using RMS.API.Models.Validators.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventFilterRequestModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        [GuidNotEmpty]
        public Guid Id { get; set; }
    }
}

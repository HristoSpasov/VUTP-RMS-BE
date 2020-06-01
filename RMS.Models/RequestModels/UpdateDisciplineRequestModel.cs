namespace RMS.API.Models.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateDisciplineRequestModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
    }
}

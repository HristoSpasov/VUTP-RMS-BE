namespace RMS.API.Models.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateSpecialtyRequestModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int Grade { get; set; }
    }
}

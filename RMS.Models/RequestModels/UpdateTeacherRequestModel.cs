namespace RMS.API.Models.RequestModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Validators.Attributes;

    public class UpdateTeacherRequestModel
    {
        [Required]
        [GuidNotEmpty]
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string AcademicTitle { get; set; }
    }
}
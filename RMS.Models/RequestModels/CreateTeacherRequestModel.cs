namespace RMS.API.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTeacherRequestModel
    {
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

namespace RMS.API.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSpecialtyRequestModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int Grade { get; set; }
    }
}

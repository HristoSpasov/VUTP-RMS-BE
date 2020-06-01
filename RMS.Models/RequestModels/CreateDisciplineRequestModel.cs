namespace RMS.API.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDisciplineRequestModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
    }
}

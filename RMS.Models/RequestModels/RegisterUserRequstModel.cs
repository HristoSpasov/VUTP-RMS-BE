namespace RMS.API.Models.RequestModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserRequstModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]

        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
    }
}

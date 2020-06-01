namespace RMS.API.Models.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRoomRequestModel
    {
        [Required]
        public string Number { get; set; }
    }
}

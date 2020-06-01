namespace RMS.API.Models.RequestModels
{
    using System;

    public class UpdateRoomRequestModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
    }
}

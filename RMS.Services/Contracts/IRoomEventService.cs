namespace RMS.Services.Contracts
{
    using RMS.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoomEventService
    {
        Task DeleteRoomsEventsByRoomIdAsync(Guid roomId);
    }
}

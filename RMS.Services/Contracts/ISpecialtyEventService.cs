namespace RMS.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISpecialtyEventService
    {
        Task DeleteSpecialtiesEventsBySpecialtyIdAsync(Guid specialtyId);
    }
}

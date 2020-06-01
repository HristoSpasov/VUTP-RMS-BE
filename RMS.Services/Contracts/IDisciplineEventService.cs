namespace RMS.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IDisciplineEventService
    {
        Task DeleteDisciplinesEventsByDisciplineIdAsync(Guid disciplineId);
    }
}

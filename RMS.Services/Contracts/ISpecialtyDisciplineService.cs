namespace RMS.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISpecialtyDisciplineService
    {
        Task DeleteSpecialtiesDisciplinesBySpecialtyIdAsync(Guid specialtyId);

        Task DeleteSpecialtiesDisciplinesByDisciplineIdAsync(Guid disciplineId);
    }
}

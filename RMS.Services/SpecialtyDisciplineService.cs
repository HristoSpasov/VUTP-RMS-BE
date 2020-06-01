namespace RMS.Services
{
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class SpecialtyDisciplineService : ISpecialtyDisciplineService
    {
        private readonly ISpecialtyDisciplineRepository specialtyDisciplineRepository;

        public SpecialtyDisciplineService(ISpecialtyDisciplineRepository specialtyDisciplineRepository)
        {
            this.specialtyDisciplineRepository = specialtyDisciplineRepository;
        }

        public async Task DeleteSpecialtiesDisciplinesByDisciplineIdAsync(Guid disciplineId)
        {
            var specialtiesDisciplinesToRemove = await this.specialtyDisciplineRepository.FindAllAsync(predicate: d => d.DisciplineId == disciplineId);

            foreach (var specialtyDiscipline in specialtiesDisciplinesToRemove)
            {
                specialtyDiscipline.IsDeleted = true;
            }

            await this.specialtyDisciplineRepository.SaveAsync();
        }

        public async Task DeleteSpecialtiesDisciplinesBySpecialtyIdAsync(Guid specialtyId)
        {
            var specialtiesDisciplinesToRemove = await this.specialtyDisciplineRepository.FindAllAsync(predicate: d => d.SpecialtyId == specialtyId);

            foreach (var specialtyDiscipline in specialtiesDisciplinesToRemove)
            {
                specialtyDiscipline.IsDeleted = true;
            }

            await this.specialtyDisciplineRepository.SaveAsync();
        }
    }
}

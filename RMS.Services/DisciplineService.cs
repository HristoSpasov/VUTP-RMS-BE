namespace RMS.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using RMS.API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <inheritdoc/>
    public class DisciplineService : IDisciplineService
    {
        /// <summary>
        /// Discipline repository private field.
        /// </summary>
        private readonly IDisciplineRepository disciplineRepository;

        /// <summary>
        /// Discipline specialty service.
        /// </summary>
        private readonly ISpecialtyDisciplineService specialtyDisciplineService;

        /// <summary>
        /// Discipline event service.
        /// </summary>
        private readonly IDisciplineEventService disciplineEventService;

        /// <summary>
        /// Auto mapper private field.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Logger service private field.
        /// </summary>
        private readonly ILogger<DisciplineService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplineService"/> class.
        /// </summary>
        /// <param name="disciplineRepository">Discipline repository parameter.</param>
        /// <param name="disciplineEventService">Discipline event service parameter.</param>
        /// <param name="specialtyDisciplineService">Discipline specialty service parameter.</param>
        /// <param name="mapper">Automapper parameter.</param>
        /// <param name="logger">Logger service parameter.</param>
        public DisciplineService(IDisciplineRepository disciplineRepository, ISpecialtyDisciplineService specialtyDisciplineService, IDisciplineEventService disciplineEventService, IMapper mapper, ILogger<DisciplineService> logger)
        {
            this.disciplineRepository = disciplineRepository;
            this.specialtyDisciplineService = specialtyDisciplineService;
            this.disciplineEventService = disciplineEventService;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ICollection<DisciplineResponseModel>> GetAllDisciplinesAsync()
        {
            var disciplines = await this.disciplineRepository.GetAllAsync(enableTracking: false);
            var mappedDisciplines = this.mapper.Map<ICollection<DisciplineResponseModel>>(disciplines);

            return mappedDisciplines;
        }

        /// <inheritdoc/>
        public async Task<DisciplineResponseModel> GetDisciplineByIdAsync(Guid id)
        {
            var discipline = await this.disciplineRepository.GetAsync(id: id, enableTracking: false);

            if (discipline == null)
            {
                this.logger.LogError($"Discipline with id '{id}' was not found in database.");
                throw new InvalidOperationException("Discipline not found!");
            }

            var mappedDiscipline = this.mapper.Map<DisciplineResponseModel>(discipline);

            return mappedDiscipline;
        }

        /// <inheritdoc/>
        public async Task CreateDisciplineAsync(CreateDisciplineRequestModel createDisciplineRequestModel)
        {
            var existingDiscipline = await this.disciplineRepository.FindAsync(predicate: d => d.Name == createDisciplineRequestModel.Name);

            if (existingDiscipline != null)
            {
                throw new InvalidOperationException($"Discipline {createDisciplineRequestModel.Name} already exists.");
            }

            var newDiscipline = this.mapper.Map<Discipline>(createDisciplineRequestModel);

            this.disciplineRepository.Add(newDiscipline);

            await this.disciplineRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateDisciplineAsync(UpdateDisciplineRequestModel updateDisciplineRequestModel)
        {
            var dbDiscipline = await this.disciplineRepository.GetAsync(updateDisciplineRequestModel.Id);

            if (dbDiscipline == null)
            {
                throw new InvalidOperationException("Invalid discipline to update!");
            }

            this.mapper.Map<UpdateDisciplineRequestModel, Discipline>(updateDisciplineRequestModel, dbDiscipline);

            await this.disciplineRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteDisciplineAsync(Guid id)
        {
            var dbDiscipline = await this.disciplineRepository.GetAsync(id);

            if (dbDiscipline == null)
            {
                throw new InvalidOperationException("Invalid discipline to delete!");
            }

            // Delete related etries.
            await this.disciplineEventService.DeleteDisciplinesEventsByDisciplineIdAsync(id);
            await this.specialtyDisciplineService.DeleteSpecialtiesDisciplinesByDisciplineIdAsync(id);

            await this.disciplineRepository.Delete(id);

            await this.disciplineRepository.SaveAsync();
        }

        public async Task<bool> GetDisciplineExistsByNameAsync(string name)
        {
            var room = await this.disciplineRepository.FindAsync(predicate: d => d.Name == name);

            return room != null;
        }
    }
}

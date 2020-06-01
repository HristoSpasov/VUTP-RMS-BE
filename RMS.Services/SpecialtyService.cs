namespace RMS.Services
{
    using API.Models.ResponseModels;
    using AutoMapper;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Repositories.Contracts;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SpecialtyService : ISpecialtyService
    {
        /// <summary>
        /// Specialty repository private field.
        /// </summary>
        private readonly ISpecialtyRepository specialtyRepository;

        private readonly ISpecialtyEventService specialtyEventService;
        private readonly ISpecialtyDisciplineService specialtyDisciplineService;

        /// <summary>
        /// Auto mapper private field.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Logger private field.
        /// </summary>
        private readonly ILogger<SpecialtyService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialtyService"/> class.
        /// </summary>
        /// <param name="specialtyRepository">Specialty repository parameter.</param>
        /// <param name="specialtyEventService">Specialty-Event service parameter.</param>
        /// <param name="mapper">Auto mapper parameter.</param>
        /// <param name="logger">Logger parameter.</param>
        public SpecialtyService(ISpecialtyRepository specialtyRepository, ISpecialtyEventService specialtyEventService, ISpecialtyDisciplineService specialtyDisciplineService, IMapper mapper, ILogger<SpecialtyService> logger)
        {
            this.specialtyRepository = specialtyRepository;
            this.specialtyEventService = specialtyEventService;
            this.specialtyDisciplineService = specialtyDisciplineService;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ICollection<SpecialtyResponseModel>> GetAllSpecialtiesAsync()
        {
            var specialties = await this.specialtyRepository.GetAllAsync(enableTracking: false);
            var mappedSpecialties = this.mapper.Map<ICollection<SpecialtyResponseModel>>(specialties);

            return mappedSpecialties;
        }

        /// <inheritdoc/>
        public async Task<SpecialtyResponseModel> GetSpecialtyByIdAsync(Guid id)
        {
            var specialty = await this.specialtyRepository.GetAsync(enableTracking: false, id: id);

            if (specialty == null)
            {
                this.logger.LogError($"Specialty with id '{id}' was not found in database.");
                throw new InvalidOperationException("Specialty not found!");
            }

            var mappedSpecialty = this.mapper.Map<SpecialtyResponseModel>(specialty);

            return mappedSpecialty;
        }

        /// <inheritdoc/>
        public async Task CreateSpecialtyAsync(CreateSpecialtyRequestModel createSpecialtyRequestModel)
        {
            var newSpecialty = this.mapper.Map<Specialty>(createSpecialtyRequestModel);

            var dbSpecialty = await this.specialtyRepository.FindAsync(predicate: s => s.Name == newSpecialty.Name && s.Grade == newSpecialty.Grade);

            if (dbSpecialty != null)
            {
                throw new InvalidOperationException("Specialty already exists");
            }

            await this.specialtyRepository.AddAsync(newSpecialty);

            await this.specialtyRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateSpecialtyAsync(UpdateSpecialtyRequestModel updateSpecialtyRequestModel)
        {
            var dbSpecialty = await this.specialtyRepository.GetAsync(updateSpecialtyRequestModel.Id);

            if (dbSpecialty == null)
            {
                throw new InvalidOperationException("There is no speciality with this parameters to update!");
            }

            this.mapper.Map<UpdateSpecialtyRequestModel, Specialty>(updateSpecialtyRequestModel, dbSpecialty);

            await this.specialtyRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteSpecialtyAsync(Guid id)
        {
            var dbSpecialty = await this.specialtyRepository.GetAsync(id);

            if (dbSpecialty == null)
            {
                throw new InvalidOperationException("Invalid Specialty to delete!");
            }

            // Delete related records
            await this.specialtyEventService.DeleteSpecialtiesEventsBySpecialtyIdAsync(id);
            await this.specialtyDisciplineService.DeleteSpecialtiesDisciplinesBySpecialtyIdAsync(id);

            await this.specialtyRepository.Delete(id);

            await this.specialtyRepository.SaveAsync();
        }
    }
}
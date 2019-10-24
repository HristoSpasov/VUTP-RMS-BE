namespace RMS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Models.ResponseModels;
    using AutoMapper;
    using Contracts;
    using Microsoft.Extensions.Logging;
    using Repositories.Contracts;

    public class SpecialtyService : ISpecialtyService
    {
        /// <summary>
        /// Specialty repository private field.
        /// </summary>
        private readonly ISpecialtyRepository specialtyRepository;

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
        /// <param name="mapper">Auto mapper parameter.</param>
        /// <param name="logger">Logger parameter.</param>
        public SpecialtyService(ISpecialtyRepository specialtyRepository, IMapper mapper, ILogger<SpecialtyService> logger)
        {
            this.specialtyRepository = specialtyRepository;
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
    }
}

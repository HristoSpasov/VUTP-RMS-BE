namespace RMS.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Models.ResponseModels;

    public interface ISpecialtyService
    {
        /// <summary>
        /// Get all specialties.
        /// </summary>
        /// <returns>List of all teachers.</returns>
        Task<ICollection<SpecialtyResponseModel>> GetAllSpecialtiesAsync();

        /// <summary>
        /// Get specialty by id.
        /// </summary>
        /// <param name="id">Teacher id parameter.</param>
        /// <returns>Teacher information.</returns>
        Task<SpecialtyResponseModel> GetSpecialtyByIdAsync(Guid id);
    }
}

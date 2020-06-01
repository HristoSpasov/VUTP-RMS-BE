namespace RMS.Services.Contracts
{
    using API.Models.ResponseModels;
    using RMS.API.Models.RequestModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Create new specialty.
        /// </summary>
        /// <param name="createSpecialtyRequestModel">Create new Specialty request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateSpecialtyAsync(CreateSpecialtyRequestModel createSpecialtyRequestModel);

        /// <summary>
        /// Update specialty data.
        /// </summary>
        /// <param name="updateSpecialtyRequestModel">Update specialty request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateSpecialtyAsync(UpdateSpecialtyRequestModel updateSpecialtyRequestModel);

        /// <summary>
        /// Delete specialty.
        /// </summary>
        /// <param name="id">Specialty id to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteSpecialtyAsync(Guid id);
    }
}
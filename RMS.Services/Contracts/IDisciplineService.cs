namespace RMS.Services.Contracts
{
    using RMS.API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Discipline service
    /// </summary>
    public interface IDisciplineService
    {
        /// <summary>
        /// Get all disciplines.
        /// </summary>
        /// <returns>List of all disciplines.</returns>
        Task<ICollection<DisciplineResponseModel>> GetAllDisciplinesAsync();

        /// <summary>
        /// Get discipline by id.
        /// </summary>
        /// <param name="id">Discipline id parameter.</param>
        /// <returns>Discipline information.</returns>
        Task<DisciplineResponseModel> GetDisciplineByIdAsync(Guid id);

        /// <summary>
        /// Create new discipline.
        /// </summary>
        /// <param name="createDisciplineRequestModel">Create new discipline request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateDisciplineAsync(CreateDisciplineRequestModel createDisciplineRequestModel);

        /// <summary>
        /// Update discipline data.
        /// </summary>
        /// <param name="updateDisciplineRequestModel">Update discipline request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateDisciplineAsync(UpdateDisciplineRequestModel updateDisciplineRequestModel);

        /// <summary>
        /// Delete discipline.
        /// </summary>
        /// <param name="id">Discipline id to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteDisciplineAsync(Guid id);

        /// <summary>
        /// Check if discipline exists by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> GetDisciplineExistsByNameAsync(string name);
    }
}

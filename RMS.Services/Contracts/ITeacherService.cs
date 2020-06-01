namespace RMS.Services.Contracts
{
    using API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Teacher service.
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// Get all teachers.
        /// </summary>
        /// <returns>List of all teachers.</returns>
        Task<ICollection<TeacherResponseModel>> GetAllTeachersAsync();

        /// <summary>
        /// Get teacher by id.
        /// </summary>
        /// <param name="id">Teacher id parameter.</param>
        /// <returns>Teacher information.</returns>
        Task<TeacherResponseModel> GetTeacherByIdAsync(Guid id);

        /// <summary>
        /// Create new teacher.
        /// </summary>
        /// <param name="createTeacherRequestModel">Create new teacher request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateTeacherAsync(CreateTeacherRequestModel createTeacherRequestModel);

        /// <summary>
        /// Update teacher data.
        /// </summary>
        /// <param name="updateTeacherRequestModel">Update teacher request model parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateTeacherAsync(UpdateTeacherRequestModel updateTeacherRequestModel);

        /// <summary>
        /// Delete teacher.
        /// </summary>
        /// <param name="id">Teacher id to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteTeacherAsync(Guid id);
    }
}
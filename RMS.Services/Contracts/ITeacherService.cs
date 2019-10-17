namespace RMS.Services.Contracts
{
    using System;
    using RMS.API.Models.ResponseModels;
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
    }
}
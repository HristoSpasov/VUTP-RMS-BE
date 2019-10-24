namespace RMS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Models.RequestModels;
    using AutoMapper;
    using Contracts;
    using Data.Entities;
    using RMS.API.Models.ResponseModels;
    using Microsoft.Extensions.Logging;
    using RMS.Repositories.Contracts;

    /// <inheritdoc/>
    public class TeacherService : ITeacherService
    {
        /// <summary>
        /// Teacher repository private field.
        /// </summary>
        private readonly ITeacherRepository teacherRepository;

        /// <summary>
        /// Auto mapper private field.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Logger service private field.
        /// </summary>
        private readonly ILogger<TeacherService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherService"/> class.
        /// </summary>
        /// <param name="teacherRepository">Teacher teacherRepository parameter.</param>
        /// <param name="mapper">Automapper parameter.</param>
        /// <param name="logger">Logger service parameter.</param>
        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeacherService> logger)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ICollection<TeacherResponseModel>> GetAllTeachersAsync()
        {
            var teachers = await this.teacherRepository.GetAllAsync(enableTracking: false);
            var mappedTeachers = this.mapper.Map<ICollection<TeacherResponseModel>>(teachers);

            return mappedTeachers;
        }

        /// <inheritdoc/>
        public async Task<TeacherResponseModel> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await this.teacherRepository.GetAsync(id: id, enableTracking: false);

            if (teacher == null)
            {
                this.logger.LogError($"Teacher with id '{id}' was not found in database.");
                throw new InvalidOperationException("Teacher not found!");
            }

            var mappedTeacher = this.mapper.Map<TeacherResponseModel>(teacher);

            return mappedTeacher;
        }

        /// <inheritdoc/>
        public async Task CreateTeacherAsync(CreateTeacherRequestModel createTeacherRequestModel)
        {
            var newTeacher = this.mapper.Map<Teacher>(createTeacherRequestModel);

            this.teacherRepository.Add(newTeacher);

            await this.teacherRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateTeacherAsync(UpdateTeacherRequestModel updateTeacherRequestModel)
        {
            var dbTeacher = await this.teacherRepository.GetAsync(updateTeacherRequestModel.Id);

            if (dbTeacher == null)
            {
                throw new InvalidOperationException("Invalid teacher to update!");
            }

            this.mapper.Map<UpdateTeacherRequestModel, Teacher>(updateTeacherRequestModel, dbTeacher);

            await this.teacherRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteTeacherAsync(Guid id)
        {
            var dbTeacher = await this.teacherRepository.GetAsync(id);

            if (dbTeacher == null)
            {
                throw new InvalidOperationException("Invalid teacher to update!");
            }

            await this.teacherRepository.Delete(id);

            await this.teacherRepository.SaveAsync();
        }
    }
}
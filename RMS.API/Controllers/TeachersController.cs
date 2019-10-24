namespace RMS.API.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.RequestModels;
    using Models.Validators.Attributes;
    using RMS.API.Models.ResponseModels;
    using RMS.Services.Contracts;

    /// <summary>
    /// Teacher actions controller.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        /// <summary>
        /// Logger private field.
        /// </summary>
        private readonly ILogger<TeachersController> logger;

        /// <summary>
        /// Teacher service private field.
        /// </summary>
        private readonly ITeacherService teacherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeachersController"/> class.
        /// </summary>
        /// <param name="logger">Logger parameter.</param>
        /// <param name="teacherService">Teacher service parameter.</param>
        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService)
        {
            this.logger = logger;
            this.teacherService = teacherService;
        }

        /// <summary>
        /// Get all teachers.
        /// </summary>
        /// <returns>Get all teachers data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TeacherResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await this.teacherService.GetAllTeachersAsync();

            return this.Ok(teachers);
        }

        /// <summary>
        /// Get teacher by id.
        /// </summary>
        /// <param name="id">Teacher id.</param>
        /// <returns>Get a teacher data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(TeacherResponseModel), StatusCodes.Status200OK)]
        [Route("{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] [GuidNotEmpty] Guid id)
        {
            try
            {
                var teacher = await this.teacherService.GetTeacherByIdAsync(id);

                return this.Ok(teacher);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Create new teacher.
        /// </summary>
        /// <param name="createTeacherRequestModel">Create teacher request model.</param>
        /// <returns>Get a teacher data.</returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequestModel createTeacherRequestModel)
        {
            await this.teacherService.CreateTeacherAsync(createTeacherRequestModel);

            return this.Ok();
        }

        /// <summary>
        /// Update teacher.
        /// </summary>
        /// <param name="updateTeacherRequestModel">Update teacher request model.</param>
        /// <returns>Get a teacher data.</returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherRequestModel updateTeacherRequestModel)
        {
            try
            {
                await this.teacherService.UpdateTeacherAsync(updateTeacherRequestModel);
            }
            catch (Exception e)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        /// <summary>
        /// Delete teacher.
        /// </summary>
        /// <param name="id">Teacher id to delete.</param>
        /// <returns>Get a teacher data.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTeacher([FromRoute] [GuidNotEmpty] Guid id)
        {
            try
            {
                await this.teacherService.DeleteTeacherAsync(id);
            }
            catch (Exception e)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
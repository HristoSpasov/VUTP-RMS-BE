namespace RMS.API.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.Validators.Attributes;
    using RMS.API.Models.ResponseModels;
    using RMS.Services.Contracts;

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
    }
}
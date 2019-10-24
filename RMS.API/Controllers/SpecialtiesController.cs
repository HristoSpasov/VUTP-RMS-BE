namespace RMS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.ResponseModels;
    using Models.Validators.Attributes;
    using Services.Contracts;

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ILogger<SpecialtiesController> logger;
        private readonly ISpecialtyService specialtyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialtiesController"/> class.
        /// </summary>
        /// <param name="logger">Logger parameter.</param>
        /// <param name="specialtyService">Specialty service parameter.</param>
        public SpecialtiesController(ILogger<SpecialtiesController> logger, ISpecialtyService specialtyService)
        {
            this.logger = logger;
            this.specialtyService = specialtyService;
        }

        /// <summary>
        /// Get all teachers.
        /// </summary>
        /// <returns>Get all teachers data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SpecialtyResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await this.specialtyService.GetAllSpecialtiesAsync();

            return this.Ok(teachers);
        }

        /// <summary>
        /// Get teacher by id.
        /// </summary>
        /// <param name="id">Teacher id.</param>
        /// <returns>Get a teacher data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(SpecialtyResponseModel), StatusCodes.Status200OK)]
        [Route("{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] [GuidNotEmpty] Guid id)
        {
            try
            {
                var teacher = await this.specialtyService.GetSpecialtyByIdAsync(id);

                return this.Ok(teacher);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}

namespace RMS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.ResponseModels;
    using Models.Validators.Attributes;
    using RMS.API.Models.RequestModels;
    using Services.Contracts;

    /// <summary>
    /// Endpoints for consuming and managing specialty entities.
    /// </summary>
    [Authorize]
    public class SpecialtiesController : BaseController
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
        /// Get all specialties.
        /// </summary>
        /// <returns>Get all specialties data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SpecialtyResponseModel>), StatusCodes.Status200OK)]
        [Authorize(Policy = "User", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllSpecialties()
        {
            var specialties = await this.specialtyService.GetAllSpecialtiesAsync();

            return this.Ok(specialties);
        }

        /// <summary>
        /// Get specialty by id.
        /// </summary>
        /// <param name="id">Specialty id.</param>
        /// <returns>Get a specialty data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(SpecialtyResponseModel), StatusCodes.Status200OK)]
        [Route("{id}")]
        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetSpecialtyById([FromRoute][GuidNotEmpty] Guid id)
        {
            var specialty = await this.specialtyService.GetSpecialtyByIdAsync(id);

            return this.Ok(specialty);
        }

        /// <summary>
        /// Create new specialty.
        /// </summary>
        /// <param name="createSpecialtyRequestModel">Create specialty request model.</param>
        /// <returns>Create specialty data resukt.</returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateSpecialty(CreateSpecialtyRequestModel createSpecialtyRequestModel)
        {
            await this.specialtyService.CreateSpecialtyAsync(createSpecialtyRequestModel);

            return this.Ok($"Specialty '{createSpecialtyRequestModel.Name} - {createSpecialtyRequestModel.Grade}' successfully created.");
        }

        /// <summary>
        /// Update specialty.
        /// </summary>
        /// <param name="updateSpecialtyRequestModel">Update specialty request model.</param>
        /// <returns>Update specialty data result.</returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateSpecialty(UpdateSpecialtyRequestModel updateSpecialtyRequestModel)
        {
            await this.specialtyService.UpdateSpecialtyAsync(updateSpecialtyRequestModel);

            return this.Ok($"Teacher changed from <name> to '{updateSpecialtyRequestModel.Name} - {updateSpecialtyRequestModel.Grade}'");
        }

        /// <summary>
        /// Delete specialty.
        /// </summary>
        /// <param name="id">Specialty id to delete.</param>
        /// <returns>Delete specialty result.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Authorize(Policy = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteSpecialty([GuidNotEmpty] Guid id)
        {
            await this.specialtyService.DeleteSpecialtyAsync(id);

            return this.Ok($"Specialty '<name>' successfully deleted.");
        }
    }
}
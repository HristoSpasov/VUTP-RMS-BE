namespace RMS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RMS.API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using RMS.API.Models.Validators.Attributes;
    using RMS.Services.Contracts;

    /// <summary>
    /// Endpoints for consuming and managing Discipline entities.
    /// </summary>
    public class DisciplinesController : BaseController
    {
        /// <summary>
        /// Logger private field.
        /// </summary>
        private readonly ILogger<DisciplinesController> logger;

        /// <summary>
        /// Discipline service private field.
        /// </summary>
        private readonly IDisciplineService disciplineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplinesController"/> class.
        /// </summary>
        /// <param name="logger">Logger parameter.</param>
        /// <param name="disciplineService">Discipline service parameter.</param>
        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineService disciplineService)
        {
            this.logger = logger;
            this.disciplineService = disciplineService;
        }

        /// <summary>
        /// Get all Disciplines.
        /// </summary>
        /// <returns>Get all Disciplines data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<DisciplineResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var disciplines = await this.disciplineService.GetAllDisciplinesAsync();

            return this.Ok(disciplines);
        }

        /// <summary>
        /// Get Discipline by id.
        /// </summary>
        /// <param name="id">Discipline id.</param>
        /// <returns>Get a Discipline data.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(DisciplineResponseModel), StatusCodes.Status200OK)]
        [Route("{id}")]
        public async Task<IActionResult> GetDisciplineById([FromRoute][GuidNotEmpty] Guid id)
        {
            var discipline = await this.disciplineService.GetDisciplineByIdAsync(id);

            return this.Ok(discipline);
        }

        /// <summary>
        /// Create new Discipline.
        /// </summary>
        /// <param name="createDisciplineRequestModel">Create Discipline request model.</param>
        /// <returns>Create Discipline resukt.</returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateDiscipline(CreateDisciplineRequestModel createDisciplineRequestModel)
        {
            await this.disciplineService.CreateDisciplineAsync(createDisciplineRequestModel);

            return this.Ok($"Discipline {createDisciplineRequestModel.Name} successfully created.");
        }

        /// <summary>
        /// Validate discipline name endpoint.
        /// </summary>
        /// <param name="name">Discipline name to validate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Route("validatedisciplinename/{name}")]
        public async Task<IActionResult> ValidateDisciplineNumber(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.BadRequest("Discipline field is required.");
            }

            if (name.Length < 3 || name.Length > 20)
            {
                return this.BadRequest("Discipline name should be between 3 and 20 characters");
            }

            var disciplineExists = await this.disciplineService.GetDisciplineExistsByNameAsync(name);

            if (disciplineExists)
            {
                return this.BadRequest($"Discipline {name} already exists.");
            }

            return this.Ok("Discipline name is valid.");
        }

        /// <summary>
        /// Update Discipline.
        /// </summary>
        /// <param name="updateDisciplineRequestModel">Update Discipline request model.</param>
        /// <returns>Uodate Discipline data result.</returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateDiscipline(UpdateDisciplineRequestModel updateDisciplineRequestModel)
        {
            await this.disciplineService.UpdateDisciplineAsync(updateDisciplineRequestModel);

            return this.Ok($"Discipline name successfully updated from <name> to {updateDisciplineRequestModel.Name}");
        }

        /// <summary>
        /// Delete Discipline.
        /// </summary>
        /// <param name="id">Discipline id to delete.</param>
        /// <returns>Delete Discipline data result.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDiscipline([GuidNotEmpty] Guid id)
        {
            await this.disciplineService.DeleteDisciplineAsync(id);

            return this.Ok($"Discipline <name> successfully deleted.");
        }
    }
}

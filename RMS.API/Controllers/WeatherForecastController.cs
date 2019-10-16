namespace RMS.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RMS.Services.Contracts;

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> logger;
        private readonly ITeacherService teacherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITeacherService teacherService)
        {
            this.logger = logger;
            this.teacherService = teacherService;
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns>Get all and so.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public string Get()
        {
            return this.teacherService.GetAll();
        }
    }
}
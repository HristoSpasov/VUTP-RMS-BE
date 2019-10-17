namespace RMS.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ILogger<SpecialtiesController> logger;

        public SpecialtiesController(ILogger<SpecialtiesController> logger)
        {
            this.logger = logger;
        }


    }
}

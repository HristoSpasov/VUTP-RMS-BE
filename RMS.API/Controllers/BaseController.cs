namespace RMS.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base controller.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}

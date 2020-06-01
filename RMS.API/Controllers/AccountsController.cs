namespace RMS.API.Controllers
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RMS.API.Models.RequestModels;
    using RMS.Services;
    using RMS.Services.Contracts;

    /// <summary>
    /// Endpoints for register and managing user accounts.
    /// </summary>
    public class AccountsController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IJwtService jwtService;
        private readonly CacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="accountService">Account serveice parameter.</param>
        /// <param name="jwtService">JWT service parameter.</param>
        /// <param name="cacheService">Cache service parameter.</param>
        public AccountsController(IAccountService accountService, IJwtService jwtService, CacheService cacheService)
        {
            this.accountService = accountService;
            this.jwtService = jwtService;
            this.cacheService = cacheService;
        }

        /// <summary>
        /// Register user endpoint.
        /// </summary>
        /// <param name="registerUserRequstModel">Register user request model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserRequstModel registerUserRequstModel)
        {
            try
            {
                await this.accountService.RegisterUserAsync(registerUserRequstModel);
                return this.Ok();
            }
            catch (InvalidOperationException ex)
            {
                return this.BadRequest(ex);
            }
            catch (Exception)
            {
                return this.BadRequest("Account create failed.");
            }
        }

        /// <summary>
        /// Login user endpoint.
        /// </summary>
        /// <param name="credentials">User credentials model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel credentials)
        {
            var identity = await this.accountService.GetClaimsIdentityAsync(credentials);

            if (identity == null)
            {
                return this.BadRequest("Invalid username or password.");
            }

            var jwt = await this.jwtService.GenerateJwtAsync(identity, credentials.UserName);

            return new OkObjectResult(jwt);
        }

        /// <summary>
        /// Validate user email endpoint.
        /// </summary>
        /// <param name="email">Email address to validate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Route("validateemailaddress/{email}")]
        public async Task<IActionResult> ValidateEmailAddress(string email)
        {
            try
            {
                var m = new MailAddress(email);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

            var user = await this.accountService.GetUserByEmailAsync(email);

            if (user != null)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        /// <summary>
        /// Validate username endpoint.
        /// </summary>
        /// <param name="userName">Username to validate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [Route("validateusername/{userName}")]
        public async Task<IActionResult> ValidateUserName(string userName)
        {
            if (userName.Length < 3 || userName.Length > 50)
            {
                return this.BadRequest();
            }

            var user = await this.accountService.GetUserByUserNameAsync(userName);

            if (user != null)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}

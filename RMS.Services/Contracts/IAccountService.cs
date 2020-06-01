namespace RMS.Services.Contracts
{
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        /// <summary>
        /// Register new user model.
        /// </summary>
        /// <param name="registerUserRequstModel">USer registration request model.</param>
        /// <returns></returns>
        Task RegisterUserAsync(RegisterUserRequstModel registerUserRequstModel);

        /// <summary>
        /// Get claims identity.
        /// </summary>
        /// <param name="loginRequestModel">Login request model.</param>
        /// <returns></returns>
        Task<ClaimsIdentity> GetClaimsIdentityAsync(LoginRequestModel loginRequestModel);

        /// <summary>
        /// Get user by email.
        /// </summary>
        /// <param name="email">User email param.</param>
        /// <returns></returns>
        Task<User> GetUserByEmailAsync(string email);


        /// <summary>
        /// Get user by username.
        /// </summary>
        /// <param name="email">Username param.</param>
        /// <returns></returns>
        Task<User> GetUserByUserNameAsync(string userName);
    }
}

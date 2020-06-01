namespace RMS.Services.Contracts
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// JWT service class.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generate JWT token.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<string> GenerateEncodedTokenAsync(string userName, ClaimsIdentity identity);
        
        /// <summary>
        /// Generate JWT token.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<string> GenerateJwtAsync(ClaimsIdentity identity, string userName);

        /// <summary>
        /// Generate claims identity.
        /// </summary>
        /// <param name="userName">User name parameter.</param>
        /// <param name="id">Id parameter.</param>
        /// <param name="roles">User roles collection.</param>
        /// <returns></returns>
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, IList<string> roles);
    }
}

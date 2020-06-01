namespace RMS.API.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Authorization configuration extension methods.
    /// </summary>
    public static class AuthorizationConfigurationExtension
    {
        /// <summary>
        /// Configure authorization claim based policies.
        /// </summary>
        /// <param name="services">Service collection parameter.</param>
        public static void ConfigureAuthorizationClaimPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim("rol", "api_access"));
            });
        }
    }
}

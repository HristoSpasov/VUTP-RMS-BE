namespace RMS.API.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using RMS.API.Infrastructure.Policies;
    using RMS.API.Infrastructure.Policies.Handlers;
    using RMS.API.Infrastructure.Policies.Requirements;

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
                options.AddPolicy(RoleType.Admin.ToString(), policy => policy.Requirements.Add(new AdminRequirement()));
                options.AddPolicy(RoleType.User.ToString(), policy => policy.Requirements.Add(new UserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, AdminHandler>();
            services.AddSingleton<IAuthorizationHandler, UserHandler>();
        }
    }
}

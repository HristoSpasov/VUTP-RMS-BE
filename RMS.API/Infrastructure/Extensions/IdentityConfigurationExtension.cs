namespace RMS.API.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RMS.Data;
    using RMS.Data.Entities;

    /// <summary>
    /// Identity configuration extetension.
    /// </summary>
    public static class IdentityConfigurationExtension
    {
        /// <summary>
        /// Configure identity.
        /// </summary>
        /// <param name="services">Service collection parameter.</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var identityBuilder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            });
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.RoleType, identityBuilder.Services);
            identityBuilder.AddRoles<IdentityRole>().AddEntityFrameworkStores<RMS_Db_Context>().AddDefaultTokenProviders();
        }
    }
}

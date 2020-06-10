namespace RMS.API.Infrastructure.Extensions
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using RMS.API.Models;
    using RMS.API.Models.Helpers;

    /// <summary>
    /// Configure authentication extensions.
    /// </summary>
    public static class AuthenticationConfigurationExtension
    {
        /// <summary>
        /// Configure JWT authentication.
        /// </summary>
        /// <param name="services">Sercice collection.</param>
        /// <param name="configuration">Aoolication configuration.</param>
        public static void ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = "Z#n2#fdM5Z8CSbgG9H!M2$Mc94P2AyvTxGRVDNP37uMfM=arnUy$Y^LQVyRbgG**ggFBx7!zzKAaD+S5UbS?by%sh=kRBEDapFpTXYPASs*^Y#?mth%KJ6A=Y8H=&Xe!qk-_ckmw$q_ygDz*P7XA=j3GSWG5uPWqNwzbgh#Z-MQmf_+B%8gL#33gKbgfEyr27H9!HMTRbj+6%GwQfJv@gcnZphj4kRHM+45yGdV!y-Sh*u5L=V5E#7z8yBZ6Y@z9";

            if (secretKey == null)
            {
                throw new Exception("Add configuration for 'RMS-API-SECRET' environment variable.");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions)).Get<JwtIssuerOptions>();

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions.Issuer;
                options.Audience = jwtAppSettingOptions.Audience;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions.Issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                // configureOptions.SaveToken = true;
            });
        }
    }
}

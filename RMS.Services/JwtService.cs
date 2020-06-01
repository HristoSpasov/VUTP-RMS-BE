namespace RMS.Services
{
    using Newtonsoft.Json;
    using RMS.API.Models.Helpers;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    /// <inheritdoc/>
    public class JwtService : IJwtService
    {
        private readonly JwtIssuerOptions jwtIssuerOptions;

        public JwtService(JwtIssuerOptions jwtIssuerOptions)
        {
            this.jwtIssuerOptions = jwtIssuerOptions;
            this.ThrowIfInvalidOptions(jwtIssuerOptions);
        }

        /// <inheritdoc/>
        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id, IList<string> roles)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim("id", id),
                new Claim("rol", "api_access"),
                new Claim("roles", string.Join(',', roles))
            });
        }

        /// <inheritdoc/>
        public async Task<string> GenerateEncodedTokenAsync(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await this.jwtIssuerOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(this.jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst("rol"),
                 identity.FindFirst("id")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: this.jwtIssuerOptions.Issuer,
                audience: this.jwtIssuerOptions.Audience,
                claims: claims,
                notBefore: this.jwtIssuerOptions.NotBefore,
                expires: this.jwtIssuerOptions.Expiration,
                signingCredentials: this.jwtIssuerOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        /// <inheritdoc/>
        public async Task<string> GenerateJwtAsync(ClaimsIdentity identity, string userName)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await this.GenerateEncodedTokenAsync(userName, identity),
                expires_in = (int)this.jwtIssuerOptions.ValidFor.TotalSeconds,
                roles = identity.Claims.Single(c => c.Type == "roles").Value
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.None });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        /// <summary>
        /// Valideate token issuer options.
        /// </summary>
        /// <param name="jwtIssuerOptions"></param>
        private void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}

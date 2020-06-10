namespace RMS.API.Infrastructure.Policies.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using RMS.API.Infrastructure.Extensions;
    using RMS.Data.Entities;

    public abstract class AbstractHandler<T> : AuthorizationHandler<T>
        where T : IAuthorizationRequirement
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        protected AbstractHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected async Task<ICollection<string>> GetUserRolesAsync(AuthorizationHandlerContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }

            var userId = this.httpContextAccessor.GetLoggedInUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return await Task.FromResult(new List<string>());
            }

            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return await Task.FromResult(new List<string>());
            }

            return await this.userManager.GetRolesAsync(user);
        }
    }
}

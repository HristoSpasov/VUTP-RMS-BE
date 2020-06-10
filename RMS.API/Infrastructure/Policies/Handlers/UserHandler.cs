﻿namespace RMS.API.Infrastructure.Policies.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using RMS.API.Infrastructure.Policies.Requirements;
    using RMS.Data.Entities;

    public class UserHandler : AbstractHandler<UserRequirement>
    {
        public UserHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base(userManager, httpContextAccessor)
        {
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            var userRoles = await this.GetUserRolesAsync(context);

            if (userRoles.Any(r => r == requirement.RoleType.ToString()))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}

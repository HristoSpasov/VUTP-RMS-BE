namespace RMS.API.Infrastructure.Policies.Requirements
{
    using Microsoft.AspNetCore.Authorization;

    public class AdminRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRequirement"/> class.
        /// </summary>
        public AdminRequirement()
        {
            this.RoleType = RoleType.Admin;
        }

        /// <summary>
        /// Gets role type.
        /// </summary>
        public RoleType RoleType { get; private set; }
    }
}

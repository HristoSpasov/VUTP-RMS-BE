namespace RMS.API.Infrastructure.Policies.Requirements
{
    using Microsoft.AspNetCore.Authorization;

    public class UserRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRequirement"/> class.
        /// </summary>
        public UserRequirement()
        {
            this.RoleType = RoleType.User;
        }

        /// <summary>
        /// Gets role type.
        /// </summary>
        public RoleType RoleType { get; private set; }
    }
}

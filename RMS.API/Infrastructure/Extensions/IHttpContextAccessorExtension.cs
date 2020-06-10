namespace RMS.API.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;

    public static class IHttpContextAccessorExtension
    {
        public static string GetLoggedInUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.User?.FindFirst("id")?.Value;
        }
    }
}

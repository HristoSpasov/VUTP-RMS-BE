namespace RMS.API.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;
    using RMS.API.Infrastructure.Filters;
    using System.Linq;

    public static class RegisterMvcExtensions
    {
        public static void RegisterMvc(this IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.Filters.Add(new ModelStateValidationActionFilterAttribute());
                opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
                opt.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status403Forbidden));

                opt.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatter = opt.OutputFormatters
                    .OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                    }
                }
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}
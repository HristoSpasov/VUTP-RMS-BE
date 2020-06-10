namespace RMS.API.Infrastructure.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var filterDescriptor = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isAuthorized = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            var authorizationRequired = context.MethodInfo.CustomAttributes.Any(a => a.AttributeType.Name == "AuthorizeAttribute");

            if (isAuthorized && authorizationRequired)
            {
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                    {
                        new Dictionary<string, IEnumerable<string>>
                        {
                             { "Bearer", Enumerable.Empty<string>() }
                        },
                    };
            }
        }
    }
}

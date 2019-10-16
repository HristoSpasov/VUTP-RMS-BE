namespace RMS.API.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ModelStateValidationActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// This method is invoked before an action method in the controller is invoked.
        /// </summary>
        /// <param name="actionContext">context for action filters</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
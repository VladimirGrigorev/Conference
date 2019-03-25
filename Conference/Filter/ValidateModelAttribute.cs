using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Conference.Filter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new Exception("Were doooooomed!");
            }
        }
    }
}
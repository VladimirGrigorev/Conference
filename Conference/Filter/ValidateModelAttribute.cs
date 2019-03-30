using System;
using System.Linq;
using Conference.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Conference.Filter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new ConfException(new
                    {errors = context.ModelState.Select(a => new { prop = a.Key, message =  a.Value.Errors[0].ErrorMessage})});
            }
        }
    }
}
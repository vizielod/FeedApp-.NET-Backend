using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLabor.Api.Filters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        protected ValidateModelFilterAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid 
                && context.HttpContext.Request.Path.HasValue
                && context.HttpContext.Request.Path.StartsWithSegments(new PathString("/api")
             ))
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            base.OnActionExecuting(context);
            
        }
    }
}

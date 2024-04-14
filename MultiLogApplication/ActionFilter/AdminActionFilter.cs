using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MultiLogApplication.ActionFilter
{
    public class AdminActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var adminClaim = context.HttpContext?.User?.Claims?.FirstOrDefault(x=>x.Type.Contains("Admin"))?.Type;
            if(adminClaim != null)
            {
                //To do : before the action executes
                await next();
                //To do : after the action executes
            }
            else
            {
                //context.HttpContext.Response.Redirect("Home/Error");
                throw new Exception("Invalid Action");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;

namespace MultiLogApplication.ActionFilter
{
    public class UserActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var adminClaim = context.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type.Contains("Admin") || x.Type.Contains("Ben"))?.Type;
            if (adminClaim != null)
            {
                //To do : before the action executes
                await next();
                //To do : after the action executes
            }
            else
            {
                throw new Exception("Invalid Action");
            }
        }
    }
}
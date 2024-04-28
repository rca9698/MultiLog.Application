using Microsoft.AspNetCore.Mvc.Filters;

namespace MultiLogApplication.ActionFilter
{
    public class SesionActiveFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var identified = context.HttpContext.User.Identity.IsAuthenticated;
            if (identified)
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

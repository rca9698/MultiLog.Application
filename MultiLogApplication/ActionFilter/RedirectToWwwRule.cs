using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;

namespace MultiLogApplication.ActionFilter
{
    public class RedirectToWwwRule : IRule
    {
        public virtual void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            if (req.Host.Host.Equals("domain.com", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            if (req.Host.Value.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            var wwwHost = new HostString($"www.{req.Host.Value}");
            var newUrl = UriHelper.BuildAbsolute(req.Scheme, wwwHost, req.PathBase, req.Path, req.QueryString);
            Serilog.Log.Debug(newUrl);
            var response = context.HttpContext.Response;
            response.StatusCode = 301;
            response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Location] = newUrl;
            context.Result = RuleResult.EndResponse;
        }
    }
}

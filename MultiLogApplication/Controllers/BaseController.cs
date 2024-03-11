using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Extensions;

namespace MultiLogApplication.Controllers
{
    public class BaseController : Controller
    {
        protected readonly long _sessionUser;
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _sessionUser = httpContextAccessor.HttpContext.Session.Get<long>("UserId");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

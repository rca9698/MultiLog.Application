using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common.LoginSignup;

namespace MultiLogApplication.Controllers
{
    public class BaseController : Controller
    {
        protected readonly long _sessionUser;
        protected readonly string _userNumber;
        public BaseController(IHttpContextAccessor httpContextAccessor, ILoginServices loginServices)
        {
            _sessionUser = httpContextAccessor.HttpContext.Session.Get<long>("UserId");
            _userNumber = httpContextAccessor.HttpContext.User.Identities?.FirstOrDefault()?.Claims?.FirstOrDefault(x => x.Type == "UserNumber")?.Value;
            if (_sessionUser == 0 && _userNumber != null)
            { 
                var otp = httpContextAccessor.HttpContext.User?.Identities?.FirstOrDefault()?.Claims?.FirstOrDefault(x => x.Type == "Otp")?.Value;

                LoginDetails details = new LoginDetails()
                {
                    UserNumber = _userNumber,
                    OTP = otp
                };
                var returnType = loginServices.LoginCred(details).Result;

                httpContextAccessor.HttpContext.Session.SetString("UserId", returnType.ReturnVal.UserId.ToString());
                httpContextAccessor.HttpContext.Session.SetString("UserNumber", returnType.ReturnVal.UserNumber);
                httpContextAccessor.HttpContext.Session.SetString("Coins", returnType.ReturnVal.Coins);
                httpContextAccessor.HttpContext.Session.SetString("OTPPass", returnType.ReturnVal.Otp);
            }
        }

    }
}

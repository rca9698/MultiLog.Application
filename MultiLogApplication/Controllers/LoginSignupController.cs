using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;

namespace MultiLogApplication.Controllers
{
    public class LoginSignupController : Controller
    {
        private readonly ILoginServices _loginServices;
        private readonly ILogger<ILoginServices> _logger;
        public LoginSignupController(ILoginServices loginServices, ILogger<ILoginServices> logger)
        {
            _loginServices = loginServices;
            _logger = logger;
        }

        public ReturnType<bool> Login(LoginDetails details)
        {
            ReturnType<bool> returnType = new ReturnType<bool>();
            try
            {
                _loginServices.LoginCred(details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at LoginSignupController > Login");
            }
            return returnType;
        }

        public ReturnType<bool> Signup(SignUpDetails details)
        {
            ReturnType<bool> returnType = new ReturnType<bool>();
            try
            {
                _loginServices.SignupCred(details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at LoginSignupController > Signup");
            }
            return returnType;
        }

    }
}

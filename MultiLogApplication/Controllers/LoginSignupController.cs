using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;
using System.Security.Claims;

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

        public async Task<ReturnType<bool>> Login(LoginDetails details)
        {
            ReturnType<bool> returnType = new ReturnType<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _loginServices.LoginCred(details);
                    if (user == null)
                    {
                        //Add logic here to display some message to user    
                        returnType.ReturnMessage = "Invalid Credential";
                        return returnType;
                    }
                    else
                    {
                        //A claim is a statement about a subject by an issuer and    
                        //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                        var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.ReturnValue.UserId)),
                        new Claim(ClaimTypes.Name, user.ReturnValue.FirstName + user.ReturnValue.LastName),
                        new Claim(ClaimTypes.Role, user.ReturnValue.Role),
                        new Claim("ContactNumber", user.ReturnValue.MobileNumber)
                };
                        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                        var principal = new ClaimsPrincipal(identity);
                        //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                        {
                            IsPersistent = false //objLoginModel.RememberLogin
                        });

                        HttpContext.Session.SetString("UserId",user.ReturnValue.UserId.ToString());
                        HttpContext.Session.SetString("UserNumber",user.ReturnValue.MobileNumber);

                        return returnType;
                    }
                }
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

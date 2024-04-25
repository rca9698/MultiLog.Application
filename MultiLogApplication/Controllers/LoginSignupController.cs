using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;
using System.Security.Claims;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Runtime.InteropServices;
using MultiLogApplication.Models.User;
using MultiLogApplication.Models.LoginModel;

namespace MultiLogApplication.Controllers
{
    public class LoginSignupController : Controller
    {
        private readonly ILoginServices _loginServices;
        private readonly ILogger<ILoginServices> _logger;
        private readonly IConfiguration _config;
        public LoginSignupController(ILoginServices loginServices, ILogger<ILoginServices> logger, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _loginServices = loginServices;
            _logger = logger;
            _config = config;
        }

        public async Task<IActionResult> SendOTP(string MobileNumber)
        {
            ReturnType<string> returnType = new ReturnType<string>();
            try
            {
                returnType = await _loginServices.SendOTP(MobileNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at LoginSignupController > SendOTP");
            }
            return Json(returnType);
        }

        public async Task<IActionResult> Login(LoginDetails details)
        {

            //Random rand = new Random();
            //string apikey = "NDk0Zjc2NDI3MDc2MzM3NDUwNDg3NDY0NzM1MzM4NjY=";
            //string numbers = "+919740859698";
            //var sentOtp = rand.Next(1000, 9999);
            //string senders = "KAPunter";

            //string message = "Your OTP is " + sentOtp + " Send by KAPunter team Kalburgi ";
            //string message1 = HttpUtility.UrlEncode(message);
            //using (var wb = new WebClient())
            //{
            //    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection() {
            //{
            //    "apikey",
            //    apikey
            //}, {
            //    "numbers",
            //    numbers
            //}, {
            //    "message",
            //    message1
            //}, {
            //    "sender",
            //    "TXTLCL"
            //}
            //    });
            //    string result = System.Text.Encoding.UTF8.GetString(response);

            //}

            //String url = "https://api.textlocal.in/send/?apikey=" + apikey + "&numbers=" + numbers + "&message=" + sentOtp + "&sender=" + senders;

            //StreamWriter mywriter = null;
            //HttpWebRequest objrequest = (HttpWebRequest)WebRequest.Create(url);

            //objrequest.Method = "POST";
            //objrequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            //objrequest.ContentType = "application/x-www-form-urlencoded";

            //mywriter = new StreamWriter(objrequest.GetRequestStream());
            //mywriter.Write(url);
            //mywriter.Close();

            ReturnType<UserDetail> returnType = new ReturnType<UserDetail>();
            ReturnType<string> returnResp = new ReturnType<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    returnType = await _loginServices.LoginCred(details);
                    if (returnType.ReturnStatus == ReturnStatus.Failure)
                    {
                        //Add logic here to display some message to user    
                        returnResp.ReturnMessage = returnType.ReturnMessage;
                        returnResp.ReturnStatus = returnType.ReturnStatus;
                    }
                    else
                    {
                        //A claim is a statement about a subject by an issuer and    
                        //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(returnType.ReturnVal.UserId)));
                        claims.Add(new Claim(ClaimTypes.Name, returnType.ReturnVal.FirstName + " " + returnType.ReturnVal.LastName));
                        claims.Add(new Claim("UserNumber", returnType.ReturnVal.UserNumber));

                        foreach (string claim in returnType.ReturnVal.Claims.Split(","))
                            claims.Add(new Claim(claim, Convert.ToString(returnType.ReturnVal.UserId)));

                        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                        var principal = new ClaimsPrincipal(identity);
                        //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                        {
                            IsPersistent = false //objLoginModel.RememberLogin
                        });

                        var otp = returnType.ReturnVal.Otp;

                        HttpContext.Session.SetString("UserId", returnType.ReturnVal.UserId.ToString());
                        HttpContext.Session.SetString("UserNumber", returnType.ReturnVal.UserNumber);
                        HttpContext.Session.SetString("Coins", returnType.ReturnVal.Coins);
                        HttpContext.Session.SetString("OTPPass", otp);


                        returnResp.ReturnMessage = returnType.ReturnMessage;
                        returnResp.ReturnStatus = returnType.ReturnStatus;
                        returnResp.ReturnVal = _config["ApiConfigs:MultilogApp:Uri"];
                    }
                } 
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at LoginSignupController > Login");
            }
            return Json(returnResp);
        }

        public ReturnType<string> Signup(SignUpDetails details)
        {
            ReturnType<string> returnType = new ReturnType<string>();
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

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return Redirect(_config["ApiConfigs:MultilogApp:Uri"]);
        }

    }
}

using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;
using MultiLogApplication.Models.LoginModel;
using MultiLogApplication.Models.User;
using System.ComponentModel;

namespace MultiLogApplication.Service
{
    public class LoginServices : ILoginServices
    {
        private readonly ILogger<LoginServices> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public LoginServices(HttpClient client, IConfiguration configuration, ILogger<LoginServices> logger, ITokenService tokenService)
        {
            _logger = logger;
            _client = client;
            _configuration = configuration;
        }

        public async Task<ReturnType<UserDetail>> LoginCred(LoginDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/LoginSignup/Login", details);
            return await response.ReadContentAs<ReturnType<UserDetail>>();
        }

        public async Task<ReturnType<string>> SendOTP(string mobileNumber)
        {
            ReturnType<Otp_Login_Model> resp = new ReturnType<Otp_Login_Model>();
            ReturnType<string> otpRespMsg = new ReturnType<string>();

            var returnMessage = "";

            var otp_resp = await _client.GetAsync($"api/LoginSignup/Generate_Otp/{mobileNumber}");
            var otpData = await otp_resp.ReadContentAs<ReturnType<Otp_Login_Model>>();

            var apiKey = otpData.ReturnVal.ApiKey;
            var otp = otpData.ReturnVal.Otp;
            var sid = otpData.ReturnVal.Sid;
            var Message = $"{otpData.ReturnVal.Message}";
            var smsBaseUrl = _configuration["ApiConfigs:SMS:Uri"];

            //http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?APIKey=ztAZri2RPU6PuyAaEJE6Lg&msisdn=919893085852&sid=SMSHUB&msg=Welcome to the Kapunter powered by SMSINDIAHUB. Your OTP for registration is 1212121&fl=0&dc=0&gwid=2
            string otpResp = "";
            if (_configuration["Environment:Type"] == "DEV")
            { 
                otpRespMsg.ReturnMessage = "OTP sent to your number As " + otp + "!!";
                otpRespMsg.ReturnStatus = ReturnStatus.Success;

                return otpRespMsg;
            }
            else
            {
                var smsUrl = $"{smsBaseUrl}?APIKey={apiKey}&msisdn={mobileNumber}&sid={sid}&msg={Message}";
                var response = await _client.GetAsync(smsUrl);
                otpResp = await response.Content.ReadAsStringAsync();
            }

            if (otpResp != null && otpResp.Contains("Failed"))
            {
                otpRespMsg.ReturnMessage = "Failed to send OTP your number!!";
                otpRespMsg.ReturnStatus = ReturnStatus.Failure;
            }
            else
            {
                otpRespMsg.ReturnMessage = "OTP sent to your number!!";
                otpRespMsg.ReturnStatus= ReturnStatus.Success;
            }

            return otpRespMsg;
        }

        public async Task<ReturnType<bool>> SignupCred(SignUpDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/LoginSignup/Signup", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }



    }
}

using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;
using System.ComponentModel;

namespace MultiLogApplication.Service
{
    public class LoginServices : ILoginServices
    {
        private readonly ILogger<LoginServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public LoginServices(HttpClient client, IConfiguration configuration, ILogger<LoginServices> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ReturnType<bool>> LoginCred(LoginDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/LoginSignup/Login", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> SignupCred(SignUpDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/LoginSignup/Signup", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }
    }
}

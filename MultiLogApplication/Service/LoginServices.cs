using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;
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

        public async Task<ReturnType<bool>> SignupCred(SignUpDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/LoginSignup/Signup", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }
    }
}

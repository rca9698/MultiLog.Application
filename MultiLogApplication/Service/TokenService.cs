using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiLogApplication.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly long _userId;
        private readonly long _otp;
        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userId = httpContextAccessor.HttpContext.Session.Get<long>("UserId");
            _otp = httpContextAccessor.HttpContext.Session.Get<long>("OTPPass");

            if(_userId == 0 || _otp == 0)
            {
                var userid = httpContextAccessor.HttpContext.User.Identities?.FirstOrDefault()?.Claims?.FirstOrDefault(x => x.Type == "Ben")?.Value;
                var otp = httpContextAccessor.HttpContext.User?.Identities?.FirstOrDefault()?.Claims?.FirstOrDefault(x => x.Type == "Otp")?.Value;

                 httpContextAccessor.HttpContext.Session.SetString("UserId", string.IsNullOrEmpty(userid) ? "0" : userid);
                 httpContextAccessor.HttpContext.Session.SetString("OTPPass", string.IsNullOrEmpty(otp) ? "0" : otp);

                _userId = httpContextAccessor.HttpContext.Session.Get<long>("UserId");
                _otp = httpContextAccessor.HttpContext.Session.Get<long>("OTPPass");
            }
        }

        public string GenerateJSONWebToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", _userId.ToString()), new Claim("otp", _otp.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void SetToken(HttpClient client)
        {
            string token = GenerateJSONWebToken();
            client.DefaultRequestHeaders.Remove("Authentication");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}

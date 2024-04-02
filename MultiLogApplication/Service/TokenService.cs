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
        public TokenService(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userId = httpContextAccessor.HttpContext.Session.Get<long>("UserId");
        }

        public string GenerateJSONWebToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", _userId.ToString()) }),
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

using Microsoft.IdentityModel.Tokens;
using MultiLogApplication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MultiLogApplication.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void SetToken(HttpClient client)
        {
            string token = GenerateJSONWebToken();
            client.DefaultRequestHeaders.Remove("Authentication");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}

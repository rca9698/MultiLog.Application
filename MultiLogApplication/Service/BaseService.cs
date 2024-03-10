using MultiLogApplication.Interfaces;

namespace MultiLogApplication.Service
{
    public class BaseService
    {
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;
        protected readonly ITokenService _tokenService;
        public BaseService(HttpClient client,IConfiguration configuration,ITokenService tokenService)
        {
            _client = client;
            _configuration = configuration;
            tokenService.SetToken(_client);
        }

    }
}

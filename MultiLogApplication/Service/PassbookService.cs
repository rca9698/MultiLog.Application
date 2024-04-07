using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Passbook;

namespace MultiLogApplication.Service
{
    public class PassbookService : BaseService, IPassbookService
    {
        private readonly ILogger<PassbookService> _logger;
        public PassbookService(HttpClient client, IConfiguration configuration, ILogger<PassbookService> logger, ITokenService tokenService) 
            : base(client, configuration, tokenService)
        {
            _logger = logger;
        }
        public async Task<ReturnType<AccountDetail>> GetPassbook(GetPassbookDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/Passbook/GetPassbook", details);
            return await response.ReadContentAs<ReturnType<AccountDetail>>();
        }

    }
}

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
        public async Task<ReturnType<PassbookDetailModel>> GetPassbookHistory(GetPassbookDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/Passbook/GetPassbookHistory", details);
            return await response.ReadContentAs<ReturnType<PassbookDetailModel>>();
        }

        public async Task<ReturnType<PassbookDetailModel>> GetPassbookHistoryById(GetPassbookHistoryById details)
        {
            var response = await _client.PostAsJsonAsync($"api/Passbook/GetPassbookHistoryById", details);
            return await response.ReadContentAs<ReturnType<PassbookDetailModel>>();
        }



    }
}

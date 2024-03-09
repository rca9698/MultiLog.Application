using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Service
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<BankAccountService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public AccountService(HttpClient client, IConfiguration configuration, ILogger<BankAccountService> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ReturnType<bool>> AddAccount(AddAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AddAccount", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> AddAccountRequest(AddAccountRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AddAccountRequest", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> DeleteAccount(DeleteAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/DeleteAccount", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<AccountDetail>> GetAccounts(GetAccounts details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/GetAccounts", details);
            return await response.ReadContentAs<ReturnType<AccountDetail>>();
        }

    }
}

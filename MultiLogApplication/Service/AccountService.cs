using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Service
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly ILogger<BankAccountService> _logger;
        public AccountService(HttpClient client, IConfiguration configuration, ILogger<BankAccountService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<AccountDetail>> GetAccounts(GetAccounts details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/GetAccounts", details);
            return await response.ReadContentAs<ReturnType<AccountDetail>>();
        }

        public async Task<ReturnType<AccountDetail>> AccountRequestList(GetAccounts details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AccountRequestList", details);
            return await response.ReadContentAs<ReturnType<AccountDetail>>();
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
    
        
    
    }
}

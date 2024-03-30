using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Service
{
    public class BankAccountService : BaseService, IBankAccountService
    {
        private readonly ILogger<BankAccountService> _logger;
        public BankAccountService(HttpClient client, IConfiguration configuration, ILogger<BankAccountService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<bool>> AddBankAccount(AddBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/AddBankAccount", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<BankDetails>> SetDefaultBankAccount(long _sessionUser, long BankDetailID)
        {
            var response = await _client.GetAsync($"api/BankAccount/SetDefaultBankAccount/{_sessionUser}/{BankDetailID}");
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<bool>> DeleteBankAccount(DeleteBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/DeleteBankAccount", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<BankDetails>> GetBankAccounts(GetBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/GetBankAccounts", details);
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<bool>> updateBankAccount(UpdateBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/UpdateBankAccount", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

    }
}

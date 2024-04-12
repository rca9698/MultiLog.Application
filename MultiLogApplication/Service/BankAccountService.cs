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

        public async Task<ReturnType<string>> AddBankAccount(AddBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/AddBankAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<BankDetails>> SetDefaultBankAccount(long _sessionUser, long BankDetailID)
        {
            var response = await _client.GetAsync($"api/BankAccount/SetDefaultBankAccount/{_sessionUser}/{BankDetailID}");
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }
         
        public async Task<ReturnType<string>> DeleteBankAccount(DeleteBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/DeleteBankAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<BankDetails>> GetBankAccounts(GetBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/GetBankAccounts", details);
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<BankDetails>> GetBankUPIDetails()
        {
            var response = await _client.GetAsync($"api/BankAccount/GetBankUPIDetails");
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<string>> updateBankAccount(UpdateBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/UpdateBankAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<BankDetails>> ActiveBankAccounts(GetBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/GetBankAccounts", details);
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<BankDetails>> DeletedBankAccounts(GetBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/DeleteBankAccount", details);
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }


        public async Task<ReturnType<BankDetails>> AdminBankAccounts()
        {
            var response = await _client.GetAsync($"api/BankAccount/GetAdminBankAccounts");
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<string>> AddUpdateAdminBankAccount(AddAdminBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/AddUpdateAdminBankAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteAdminBankAccount(DeleteAdminBankAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/DeleteAdminBankAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> SetDefaultAdminBankAccount(long _sessionUser, long BankDetailID)
        {
            var response = await _client.GetAsync($"api/BankAccount/SetDefaultAdminBankAccount/{_sessionUser}/{BankDetailID}");
            return await response.ReadContentAs<ReturnType<string>>();
        }



        public async Task<ReturnType<BankDetails>> AdminUpiAccounts()
        {
            var response = await _client.GetAsync($"api/BankAccount/GetAdminUpiAccount");
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<string>> AddUpdateAdminUpiAccount(AddUpdateAdminUpiAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/AddUpdateAdminUpiAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteAdminUpiAccount(long _sessionUser, long UpiId)
        {
            var response = await _client.GetAsync($"api/BankAccount/DeleteAdminUpiAccount/{_sessionUser}/{UpiId}");
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> SetDefaultAdminUpiAccount(long _sessionUser, long UpiId)
        {
            var response = await _client.GetAsync($"api/BankAccount/SetDefaultAdminBankAccount/{_sessionUser}/{UpiId}");
            return await response.ReadContentAs<ReturnType<string>>();
        }



        public async Task<ReturnType<BankDetails>> GetAdminQRCode()
        {
            var response = await _client.GetAsync($"api/BankAccount/GetAdminQRCode");
            return await response.ReadContentAs<ReturnType<BankDetails>>();
        }

        public async Task<ReturnType<string>> AddAdminQRCode(long sessionUser,string userName)
        {
            var response = await _client.GetAsync($"api/BankAccount/AddUpdateAdminQRCode/{sessionUser}/{userName}");
            return await response.ReadContentAs<ReturnType<string>>();
        }

    }
}

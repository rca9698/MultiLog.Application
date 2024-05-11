using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Service
{
    public class IDService : BaseService, IIDService
    {
        private readonly ILogger<BankAccountService> _logger;
        public IDService(HttpClient client, IConfiguration configuration, ILogger<BankAccountService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<IDDetail>> GetIDs(GetIDs details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/GetIDs", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }

        public async Task<ReturnType<IDRequest>> IDRequestList(GetIDs details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/IDRequestList", details);
            return await response.ReadContentAs<ReturnType<IDRequest>>();
        }

        public async Task<ReturnType<IDRequest>> IDRequestDetails(long AccountRequestId)
        {
            var response = await _client.GetAsync($"api/Account/IDRequestDetails/{AccountRequestId}");
            return await response.ReadContentAs<ReturnType<IDRequest>>();
        }

        public async Task<ReturnType<string>> AddID(AddAccount details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AddID", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> AddIDRequest(AddIDRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AddIDRequest", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteID(DeleteID details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/DeleteID", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteIDRequest(DeleteIDRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/DeleteIDRequest", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<IDDetail>> ListIDChangePassword(GetIDs details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/ListIDChangePassword", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }

        public async Task<ReturnType<IDDetail>> ListIDCloseRequest(GetIDs details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/ListIDCloseRequest", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }

        public async Task<ReturnType<IDDetail>> AddChangeIDPassword(ChangeIDPasswordRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AddChangeIDPassword", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }

        public async Task<ReturnType<IDDetail>> AddCloseID(CloseIDRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/AddCloseID", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }

        public async Task<ReturnType<IDDetail>> ConfirmChangeIDPassword(ChangeIDPasswordRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/ConfirmChangeIDPassword", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }

        public async Task<ReturnType<IDDetail>> ConfirmCloseID(CloseIDRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Account/ConfirmCloseID", details);
            return await response.ReadContentAs<ReturnType<IDDetail>>();
        }
    }
}

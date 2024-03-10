using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.DropDown;

namespace MultiLogApplication.Service
{
    public class DropDownService : BaseService, IDropDownService
    {
        private readonly ILogger<DropDownService> _logger;
        public DropDownService(HttpClient client, IConfiguration configuration, ILogger<DropDownService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<DropDownDetails>> StatusTypes(GetStatusType details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/AddBankAccount", details);
            return await response.ReadContentAs<ReturnType<DropDownDetails>>();
        }

        public async Task<ReturnType<DropDownDetails>> TransactionTypes(TransactionType details)
        {
            var response = await _client.PostAsJsonAsync($"api/BankAccount/AddBankAccount", details);
            return await response.ReadContentAs<ReturnType<DropDownDetails>>();
        }
    }
}

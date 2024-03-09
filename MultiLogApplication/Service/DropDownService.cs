using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.DropDown;

namespace MultiLogApplication.Service
{
    public class DropDownService : IDropDownService
    {
        private readonly ILogger<DropDownService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public DropDownService(HttpClient client, IConfiguration configuration, ILogger<DropDownService> logger)
        {
            _client = client;
            _configuration = configuration;
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

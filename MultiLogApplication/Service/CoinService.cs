using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common.LoginSignup;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Extensions;
using MultiLogApplication.Models.Coin;

namespace MultiLogApplication.Service
{
    public class CoinService : ICoinService
    {
        private readonly ILogger<LoginServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public CoinService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<ReturnType<CoinDetails>> GetTransaction(ListCoinModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/GetTransaction", details);
            return await response.ReadContentAs<ReturnType<CoinDetails>>();
        }

        public async Task<ReturnType<bool>> AddCoins(InsertCoinDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/AddCoins", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> DeleteCoins(InsertCoinDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/DeleteCoins", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }


    }
}

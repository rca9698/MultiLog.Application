using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common.LoginSignup;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Extensions;
using MultiLogApplication.Models.Coin;

namespace MultiLogApplication.Service
{
    public class CoinService : BaseService, ICoinService
    {
        private readonly ILogger<LoginServices> _logger;
        public CoinService(HttpClient client, IConfiguration configuration, ILogger<LoginServices> logger, ITokenService tokenService)
            : base(client, configuration, tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<CoinsRequestModel>> GetTransaction(ListCoinModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/GetTransaction", details);
            return await response.ReadContentAs<ReturnType<CoinsRequestModel>>();
        }

        public async Task<ReturnType<CoinsRequestModel>> GetCoinsRequest(DepositWithdrawCoinsRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/GetCoinsRequest", details);
            return await response.ReadContentAs<ReturnType<CoinsRequestModel>>();
        }

        public async Task<ReturnType<bool>> AddCoinsRequest(InsertCoinRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/AddCoinsRequest", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> DeleteCoinsRequest(DeleteCoinRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/DeleteCoinsRequest", details);
            return await response.ReadContentAs<ReturnType<bool>>();
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

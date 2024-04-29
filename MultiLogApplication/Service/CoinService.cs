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

        public async Task<ReturnType<CoinsToAccountRequestModel>> GetCoinsToAccountRequest(int coinType,long sessionUser)
        {
            var response = await _client.GetAsync($"api/Coin/GetCoinsToAccountRequest/{coinType}/{sessionUser}");
            return await response.ReadContentAs<ReturnType<CoinsToAccountRequestModel>>();
        }

        public async Task<ReturnType<string>> AddCoinsRequest(InsertCoinRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/AddCoinsRequest", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> WithDrawCoinsRequest(WithdrawCoinRequest details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/WithDrawCoinsRequest", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> UpdateCoins(UpdateCoinDetails details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/UpdateCoins", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteCoins(DeleteCoinsModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/DeleteCoins", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> UpdateCoinsToAccountRequest(UpdateCoinsToAccountRequestModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/UpdateCoinsToAccountRequest", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> UpdateCoinsToAccount(UpdateCoinsToAccountModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/UpdateCoinsToAccount", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteAccountRequestCoins(DeleteRequestCoinsModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/DeleteAccountRequestCoins", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteRequestCoins(DeleteRequestCoinsModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Coin/DeleteRequestCoins", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }
    }
}

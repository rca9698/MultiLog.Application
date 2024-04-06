using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface ICoinService
    {
        Task<ReturnType<CoinsRequestModel>> GetTransaction(ListCoinModel details);
        Task<ReturnType<CoinsRequestModel>> GetCoinsRequest(DepositWithdrawCoinsRequest details);
        Task<ReturnType<string>> AddCoinsRequest(InsertCoinRequest details);
        Task<ReturnType<string>> WithDrawCoinsRequest(DeleteCoinRequest details);
        Task<ReturnType<string>> AddCoins(InsertCoinDetails details);
        Task<ReturnType<string>> DeleteCoins(InsertCoinDetails details);
        Task<ReturnType<string>> AddCoinsToAccountRequest(InsertCoinToAccountRequestModel details);
        Task<ReturnType<string>> WithDrawToAccountRequest(DeleteCoinToAccountRequest details);

    }
}

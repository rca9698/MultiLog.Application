using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface ICoinService
    {
        Task<ReturnType<CoinsRequestModel>> GetTransaction(ListCoinModel details);
        Task<ReturnType<CoinsRequestModel>> GetCoinsRequest(DepositWithdrawCoinsRequest details);
        Task<ReturnType<bool>> AddCoinsRequest(InsertCoinRequest details);
        Task<ReturnType<bool>> DeleteCoinsRequest(DeleteCoinRequest details);
        Task<ReturnType<bool>> AddCoins(InsertCoinDetails details);
        Task<ReturnType<bool>> DeleteCoins(InsertCoinDetails details);
    }
}

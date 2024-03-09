using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface ICoinService
    {
        Task<ReturnType<CoinDetails>> GetTransaction(ListCoinModel details);
        Task<ReturnType<bool>> AddCoins(InsertCoinDetails details);
        Task<ReturnType<bool>> DeleteCoins(InsertCoinDetails details);
    }
}

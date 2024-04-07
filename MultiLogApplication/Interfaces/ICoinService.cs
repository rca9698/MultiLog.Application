using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface ICoinService
    {
        Task<ReturnType<CoinsRequestModel>> GetTransaction(ListCoinModel details);
        Task<ReturnType<CoinsRequestModel>> GetCoinsRequest(DepositWithdrawCoinsRequest details);
        Task<ReturnType<CoinsToAccountRequestModel>> GetCoinsToAccountRequest(int coinType, long sessionUser);
        Task<ReturnType<string>> AddCoinsRequest(InsertCoinRequest details);
        Task<ReturnType<string>> WithDrawCoinsRequest(DeleteCoinRequest details);
        Task<ReturnType<string>> UpdateCoins(UpdateCoinDetails details);
        Task<ReturnType<string>> UpdateCoinsToAccountRequest(UpdateCoinsToAccountRequestModel details);
        Task<ReturnType<string>> UpdateCoinsToAccount(UpdateCoinsToAccountModel details);
    }
}

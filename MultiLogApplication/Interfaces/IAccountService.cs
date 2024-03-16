using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface IAccountService
    {
        Task<ReturnType<AccountDetail>> GetAccounts(GetAccounts details);
        Task<ReturnType<AccountDetail>> AccountRequestList(GetAccounts details);
        Task<ReturnType<bool>> AddAccount(AddAccount details);
        Task<ReturnType<bool>> AddAccountRequest(AddAccountRequest details);
        Task<ReturnType<bool>> DeleteAccount(DeleteAccount details);
    }
}

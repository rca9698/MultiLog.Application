using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Interfaces
{
    public interface IAccountService
    {
        Task<ReturnType<AccountDetail>> GetAccounts(GetAccounts details);
        Task<ReturnType<AccountRequest>> AccountRequestList(GetAccounts details);
        Task<ReturnType<AccountRequest>> AccountRequestDetails(long AccountRequestId);
        Task<ReturnType<bool>> AddAccount(AddAccount details);
        Task<ReturnType<bool>> AddAccountRequest(AddAccountRequest details);
        Task<ReturnType<bool>> DeleteAccount(DeleteAccount details);
    }
}

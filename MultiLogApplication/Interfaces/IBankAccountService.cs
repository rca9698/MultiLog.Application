using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface IBankAccountService
    {
        Task<ReturnType<BankDetails>> GetBankAccounts(GetBankAccount entity);
        Task<ReturnType<bool>> AddBankAccount(AddBankAccount entity);
        Task<ReturnType<BankDetails>> SetDefaultBankAccount(long _sessionUser,long BankDetailID);
        Task<ReturnType<bool>> DeleteBankAccount(DeleteBankAccount entity);
        Task<ReturnType<bool>> updateBankAccount(UpdateBankAccount entity);
    }
}

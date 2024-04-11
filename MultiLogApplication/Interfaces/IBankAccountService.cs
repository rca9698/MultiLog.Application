using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Interfaces
{
    public interface IBankAccountService
    {
        Task<ReturnType<BankDetails>> GetBankAccounts(GetBankAccount entity);
        Task<ReturnType<BankDetails>> GetBankUPIDetails();
        Task<ReturnType<BankDetails>> ActiveBankAccounts(GetBankAccount details);
        Task<ReturnType<BankDetails>> DeletedBankAccounts(GetBankAccount details);
        Task<ReturnType<string>> AddBankAccount(AddBankAccount entity);
        Task<ReturnType<BankDetails>> SetDefaultBankAccount(long _sessionUser,long BankDetailID);
        Task<ReturnType<string>> DeleteBankAccount(DeleteBankAccount entity);
        Task<ReturnType<string>> updateBankAccount(UpdateBankAccount entity);
        Task<ReturnType<BankDetails>> AdminBankAccounts();
        Task<ReturnType<string>> AddUpdateAdminBankAccount(AddAdminBankAccount entity);
    }
}

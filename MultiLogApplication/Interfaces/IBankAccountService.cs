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
        Task<ReturnType<BankDetails>> GetBankAccountById(long BankDetailID);
        Task<ReturnType<string>> DeleteBankAccount(DeleteBankAccount entity);
        Task<ReturnType<string>> updateBankAccount(UpdateBankAccount entity);

        Task<ReturnType<BankDetails>> AdminBankAccounts();
        Task<ReturnType<string>> AddUpdateAdminBankAccount(AddAdminBankAccount entity);
        Task<ReturnType<string>> DeleteAdminBankAccount(DeleteAdminBankAccount entity);
        Task<ReturnType<string>> SetDefaultAdminBankAccount(long _sessionUser, long BankDetailID);


        Task<ReturnType<BankDetails>> AdminUpiAccounts();
        Task<ReturnType<string>> AddUpdateAdminUpiAccount(AddUpdateAdminUpiAccount entity);
        Task<ReturnType<string>> DeleteAdminUpiAccount(long _sessionUser, long UpiId);
        Task<ReturnType<string>> SetDefaultAdminUpiAccount(long _sessionUser, long UpiId);


        Task<ReturnType<BankDetails>> GetAdminQRCode();
        Task<ReturnType<string>> AddAdminQRCode(long sessionUser, string userName);

    }
}

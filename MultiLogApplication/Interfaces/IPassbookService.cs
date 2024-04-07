using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Passbook;

namespace MultiLogApplication.Interfaces
{
    public interface IPassbookService
    {
        Task<ReturnType<PassbookDetailModel>> GetPassbookHistory(GetPassbookDetails details);
    }
}

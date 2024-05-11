using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Interfaces
{
    public interface IIDService
    {
        Task<ReturnType<IDDetail>> GetIDs(GetIDs details);
        Task<ReturnType<IDRequest>> IDRequestList(GetIDs details);
        Task<ReturnType<IDRequest>> IDRequestDetails(long AccountRequestId);
        Task<ReturnType<string>> AddID(AddAccount details);
        Task<ReturnType<string>> AddIDRequest(AddIDRequest details);
        Task<ReturnType<string>> DeleteID(DeleteID details);
        Task<ReturnType<string>> DeleteIDRequest(DeleteIDRequest details);

        Task<ReturnType<IDDetail>> ListIDChangePassword(GetIDs details);
        Task<ReturnType<IDDetail>> ListIDCloseRequest(GetIDs details);
        Task<ReturnType<IDDetail>> AddChangeIDPassword(ChangeIDPasswordRequest details);
        Task<ReturnType<IDDetail>> AddCloseID(CloseIDRequest details);
        Task<ReturnType<IDDetail>> ConfirmChangeIDPassword(ChangeIDPasswordRequest details);
        Task<ReturnType<IDDetail>> ConfirmCloseID(CloseIDRequest details);

    }
}

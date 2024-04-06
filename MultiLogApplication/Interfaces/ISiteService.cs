using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Interfaces
{
    public interface ISiteService
    {
        Task<ReturnType<SiteDetail>> Getsites(ListSites details);
        Task<ReturnType<bool>> AddSite(AddSite details);
        Task<ReturnType<bool>> DeleteSite(DeleteSite details);
        Task<ReturnType<bool>> UpdateSite(UpdateSite details);
        Task<ReturnType<SiteDetail>> GetUserListSites();
        Task<ReturnType<SiteDetail>> GetUserListSiteById(long userId);
        Task<ReturnType<AccountDetail>> ViewThisSiteDetails(ViewThisSiteDetailModel details);
    }
}

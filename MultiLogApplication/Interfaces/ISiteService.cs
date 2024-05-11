using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Interfaces
{
    public interface ISiteService
    {
        Task<ReturnType<SiteDetail>> Getsites(ListSites details);
        Task<ReturnType<string>> AddSite(AddSite details);
        Task<ReturnType<string>> DeleteSite(DeleteSite details);
        Task<ReturnType<string>> UpdateSite(UpdateSite details);
        Task<ReturnType<SiteDetail>> GetUserListSites();
        Task<ReturnType<SiteDetail>> GetUserListSiteById(long userId);
        Task<ReturnType<IDDetail>> ViewThisSiteDetails(ViewThisSiteDetailModel details);
    }
}

using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Service
{
    public class SiteService : BaseService, ISiteService
    {
        private readonly ILogger<SiteService> _logger;
        public SiteService(HttpClient client, IConfiguration configuration, ILogger<SiteService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<string>> AddSite(AddSite details)
        {
            var response = await _client.PostAsJsonAsync($"api/Site/AddSite", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteSite(DeleteSite details)
        {
            var response = await _client.PostAsJsonAsync($"api/Site/DeleteSite", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<SiteDetail>> Getsites(ListSites details)
        {
            var response = await _client.PostAsJsonAsync($"api/Site/Getsites", details);
            return await response.ReadContentAs<ReturnType<SiteDetail>>();
        }

        public async Task<ReturnType<string>> UpdateSite(UpdateSite details)
        {
            var response = await _client.PostAsJsonAsync($"api/Site/UpdateSite", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<SiteDetail>> GetUserListSiteById(long userId)
        {
            var response = await _client.GetAsync($"api/Site/GetUserListSiteById/{userId}");
            return await response.ReadContentAs<ReturnType<SiteDetail>>();
        }

        public async Task<ReturnType<SiteDetail>> GetUserListSites()
        {
            var response = await _client.GetAsync($"api/Site/GetUserListSites");
            return await response.ReadContentAs<ReturnType<SiteDetail>>();
        }

        public async Task<ReturnType<AccountDetail>> ViewThisSiteDetails(ViewThisSiteDetailModel details)
        {
            var response = await _client.PostAsJsonAsync($"api/Site/ViewThisSiteDetails", details);
            return await response.ReadContentAs<ReturnType<AccountDetail>>();
        }


    }
}

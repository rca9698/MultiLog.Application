using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Dashboard;

namespace MultiLogApplication.Service
{
    public class HomeService : BaseService, IHomeService
    {
        private readonly ILogger<UserService> _logger;
        public HomeService(HttpClient client, IConfiguration configuration, ILogger<UserService> logger, ITokenService tokenService) : base(client, configuration, tokenService)
        {
            _logger = logger;
        }
        public async Task<ReturnType<DashboardImages>> DashboardImages()
        {
            var response = await _client.GetAsync($"api/Home/GetDashboardImages");
            return await response.ReadContentAs<ReturnType<DashboardImages>>();
        }

        public async Task<ReturnType<string>> InsertDahboardImages(InsertDashboardImages obj)
        {
            var response = await _client.PostAsJsonAsync($"api/Home/InsertDashboardImages", obj);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteDahboardImages(string DocId)
        {
            var response = await _client.GetAsync($"api/Home/DeleteDahboardImages/{DocId}");
            return await response.ReadContentAs<ReturnType<string>>();
        }
    }
}

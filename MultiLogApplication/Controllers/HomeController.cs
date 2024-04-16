using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Dashboard;
using MultiLogApplication.Models.SiteDetails;
using System.Diagnostics;

namespace MultiLogApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISiteService _siteService;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ISiteService siteService) : base(httpContextAccessor)
        {
            _logger = logger;
            _siteService = siteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            DashboardModel dashboardModel = new DashboardModel();
            ReturnType<SiteDetail> res = new ReturnType<SiteDetail>();
            try
            {
                var data = _sessionUser != 0 ? await _siteService.GetUserListSiteById(_sessionUser) : new ReturnType<SiteDetail>();
                dashboardModel.SiteDetail = data?.ReturnList?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at HomeController > Dashboard");
            }
            return View(dashboardModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
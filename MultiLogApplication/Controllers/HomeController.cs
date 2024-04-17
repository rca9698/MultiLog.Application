using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Dashboard;
using MultiLogApplication.Models.SiteDetails;
using MultiLogApplication.Service;
using System.Diagnostics;

namespace MultiLogApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISiteService _siteService;
        private readonly IHomeService _homeService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, ISiteService siteService, IHomeService homeService, IWebHostEnvironment hostingEnvironment, IConfiguration configuration) : base(httpContextAccessor)
        {
            _logger = logger;
            _siteService = siteService;
            _homeService = homeService;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            DashboardModel dashboardModel = new DashboardModel();
            ReturnType<SiteDetail> resSites = new ReturnType<SiteDetail>();
            ReturnType<DashboardImages> resDBImages = new ReturnType<DashboardImages>();
            try
            {
                resDBImages = await _homeService.DashboardImages();
                if (_sessionUser != 0)
                {
                    resSites = await _siteService.GetUserListSiteById(_sessionUser);
                    
                    dashboardModel.SiteDetail = resSites.ReturnList;
                }
                dashboardModel.DashboardImages = resDBImages.ReturnList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at HomeController > Dashboard");
            }
            return View(dashboardModel);
        }

        public async Task<IActionResult> InsertDahboardImages(InsertDashboardImages obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                string BasePath = _hostingEnvironment.WebRootPath;
                //string wwwPath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:DashboardImages:Path"];
                string fileName = Guid.NewGuid().ToString();
                string iconContentPath = BasePath + contentPath;
                if (!Directory.Exists(iconContentPath))
                {
                    Directory.CreateDirectory(iconContentPath);
                }
                var extenstion = obj.File.FileName.Split(".").LastOrDefault();
                string docName = iconContentPath + "\\" + Path.GetFileName(fileName + "." + extenstion);
                using (FileStream stream = new FileStream(Path.Combine(docName), FileMode.Create))
                {
                    obj.File.CopyTo(stream);
                }

                obj.DocumentDetailId = fileName;
                obj.FileExtenstion = extenstion;
                res = await _homeService.InsertDahboardImages(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at HomeController > InsertDahboardImages");
            }
            return View();
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
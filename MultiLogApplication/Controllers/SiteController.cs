using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.NotificationDetails;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Controllers
{
    public class SiteController : BaseController
    {
        private readonly ISiteService _siteService;
        private readonly ILogger<SiteController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SiteController(ISiteService siteService, ILogger<SiteController> logger
            , IHttpContextAccessor httpContextAccessor, IConfiguration configuration
            , IWebHostEnvironment hostingEnvironment) 
            : base(httpContextAccessor)
        {
            _siteService = siteService;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Getsites(ListSites obj)
        {
            ReturnType<SiteDetail> res = new ReturnType<SiteDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _siteService.Getsites(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at SiteController > Getsites");
            }
            return PartialView("~/Views/Site/ListSites.cshtml", res);
        }

        public async Task<IActionResult> AddSite(AddSite obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                string BasePath = _hostingEnvironment.WebRootPath;
                //string wwwPath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:SiteIcon:Path"];
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

                obj.SessionUser = _sessionUser;
                obj.ImageName = obj.File.FileName;
                obj.DocumentDetailId = fileName;
                obj.FileExtenstion = extenstion;
                obj.ImageSize = obj.File.Length.ToString();
                res = await _siteService.AddSite(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoinsRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> UpdateSite(UpdateSite obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _siteService.UpdateSite(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at SiteController > UpdateSite");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteSite(DeleteSite obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _siteService.DeleteSite(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at SiteController > DeleteSite");
            }
            return Json(res);
        }


        //User SIte Sites
        public async Task<IActionResult> GetUserListSites()
        {
            ReturnType<SiteDetail> res = new ReturnType<SiteDetail>();
            try
            {
                res = await _siteService.GetUserListSites();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at SiteController > GetUserListSites");
            }
            return PartialView("~/Views/Site/UserListSites.cshtml", res);
        }

        public async Task<IActionResult> GetUserListSiteById()
        {
            ReturnType<SiteDetail> res = new ReturnType<SiteDetail>();
            try
            {
                res = await _siteService.GetUserListSiteById(_sessionUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at SiteController > GetUserListSiteById");
            }
            return PartialView("~/Views/Site/GetUserListSiteById.cshtml", res);
        }

    }
}

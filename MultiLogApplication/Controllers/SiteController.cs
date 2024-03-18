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
        public SiteController(ISiteService siteService, ILogger<SiteController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor)
        {
            _siteService = siteService;
            _logger = logger;
            _configuration = configuration;
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
            return View("~/Views/Site/ListSites.cshtml", res);
        }

        public async Task<IActionResult> AddSite(AddSite obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                string wwwPath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:SiteIcon:Path"];
                string fileName = Guid.NewGuid().ToString();
                if (!Directory.Exists(wwwPath + contentPath))
                {
                    Directory.CreateDirectory(wwwPath+contentPath);
                }
                var extenstion = obj.File.FileName.Split(".").LastOrDefault();
                string docName = wwwPath + contentPath + "\\" + Path.GetFileName(fileName + "." + extenstion);
                using (FileStream stream = new FileStream(Path.Combine(wwwPath,contentPath, docName), FileMode.Create))
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
    }
}

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
        public SiteController(ISiteService siteService, ILogger<SiteController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _siteService = siteService;
            _logger = logger;
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
            return View(res);
        }

        public async Task<IActionResult> AddSite(AddSite obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _siteService.AddSite(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at SiteController > AddSite");
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

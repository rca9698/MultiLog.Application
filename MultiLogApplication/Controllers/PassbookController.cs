using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Controllers
{
    public class PassbookController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<PassbookController> _logger;
        public PassbookController(IAccountService accountService, ILogger<PassbookController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewDetailsPV(long siteId)
        {
            ReturnType<AccountDetail> res = new ReturnType<AccountDetail>();
            try
            {
                ViewThisSiteDetailModel obj = new ViewThisSiteDetailModel()
                {
                    SiteId = siteId,
                    UserId = _sessionUser
                };
                res = await _siteService.ViewThisSiteDetails(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at PassbookController > ViewDetailsPV");
            }
            return PartialView("~/Views/Site/ViewDetailsPV.cshtml", res);
        }
    }
}

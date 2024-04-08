using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Passbook;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Controllers
{
    public class PassbookController : BaseController
    {
        private readonly IPassbookService _passbookService;
        private readonly ILogger<PassbookController> _logger;
        public PassbookController(IPassbookService passbookService, ILogger<PassbookController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _passbookService = passbookService;
            _logger = logger;
        }

        public IActionResult Index(string viewType)
        {
            return View("Views/Passbook/Index.cshtml", viewType);
        }
        public async Task<IActionResult> ViewPanel()
        {
            ReturnType<PassbookDetailModel> res = new ReturnType<PassbookDetailModel>();
            try
            {
                GetPassbookDetails obj = new GetPassbookDetails()
                {
                    UserId = _sessionUser,
                    SessionUser = _sessionUser
                };
                res = await _passbookService.GetPassbookHistory(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at PassbookController > ViewPanel");
            }
            return PartialView("~/Views/Passbook/ViewPanel.cshtml", res);
        }
    }
}

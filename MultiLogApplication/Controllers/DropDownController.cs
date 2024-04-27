using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.DropDown;

namespace MultiLogApplication.Controllers
{
    public class DropDownController : BaseController
    {
        private readonly IDropDownService _dropDownService;
        private readonly ILogger<DropDownController> _logger;
        public DropDownController(IDropDownService coinService, ILogger<DropDownController> logger, IHttpContextAccessor httpContextAccessor, ILoginServices loginServices) : base(httpContextAccessor, loginServices)
        {
            _dropDownService = coinService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCoins(TransactionType obj)
        {
            ReturnType<DropDownDetails> res = new ReturnType<DropDownDetails>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _dropDownService.TransactionTypes(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoins");
            }
            return Json(res);
        }

        public async Task<IActionResult> StatusTypes(GetStatusType obj)
        {
            ReturnType<DropDownDetails> res = new ReturnType<DropDownDetails>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _dropDownService.StatusTypes(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetStatusType");
            }
            return Json(res);
        }

    }
}

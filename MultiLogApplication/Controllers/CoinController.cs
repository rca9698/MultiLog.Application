using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Service;

namespace MultiLogApplication.Controllers
{
    public class CoinController : Controller
    {
        private readonly ICoinService _coinService;
        private readonly ILogger<CoinController> _logger;
        private readonly long _sessionUser;
        public CoinController(ICoinService coinService, ILogger<CoinController> logger)
        {
            _coinService = coinService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCoins(InsertCoinDetails obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.AddCoins(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoins");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteCoins(InsertCoinDetails obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.DeleteCoins(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > DeleteCoins");
            }
            return Json(res);
        }

        public async Task<IActionResult> GetTransaction(ListCoinModel obj)
        {
            ReturnType<CoinDetails> res = new ReturnType<CoinDetails>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.GetTransaction(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetTransaction");
            }
            return Json(res);
        }

    }
}

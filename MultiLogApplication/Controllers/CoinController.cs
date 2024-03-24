using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Service;

namespace MultiLogApplication.Controllers
{
    public class CoinController : BaseController
    {
        private readonly ICoinService _coinService;
        private readonly ILogger<CoinController> _logger;
        private readonly IConfiguration _configuration;
        public CoinController(ICoinService coinService, ILogger<CoinController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor)
        {
            _coinService = coinService;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index(string viewType)
        {
            return View("/Views/Coin/Index.cshtml", viewType);
        }

        public async Task<IActionResult> GetDepositCoinsRequest(DepositWithdrawCoinsRequest obj)
        {
            ReturnType<CoinsRequestModel> res = new ReturnType<CoinsRequestModel>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.CoinType = 1;
                res = await _coinService.GetCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetDepositCoinsRequest");
            }
            return PartialView("~/Views/Coin/DepositCoinsRequests.cshtml", res);
        }

        public async Task<IActionResult> GetWithdrawCoinsRequest(DepositWithdrawCoinsRequest obj)
        {
            ReturnType<CoinsRequestModel> res = new ReturnType<CoinsRequestModel>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.CoinType = 0;
                res = await _coinService.GetCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetWithdrawCoinsRequest");
            }
            return PartialView("~/Views/Coin/WithdrawCoinsRequests.cshtml", res);
        }

        public async Task<IActionResult> GetTransaction(ListCoinModel obj)
        {
            ReturnType<CoinsRequestModel> res = new ReturnType<CoinsRequestModel>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.GetTransaction(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetTransaction");
            }
            return PartialView("~/Views/Coin/CoinsHistory.cshtml", res);
        }

        public async Task<IActionResult> AddCoinsRequest(InsertCoinRequest obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                string wwwPath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:BasePath:Path"];

                if (!Directory.Exists(contentPath))
                {
                    Directory.CreateDirectory(contentPath);
                }
                string photoName = Path.GetFileName(obj.File.FileName);
                using (FileStream stream = new FileStream(Path.Combine(contentPath, photoName), FileMode.Create))
                {
                    obj.File.CopyTo(stream);
                }

                return Json(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoinsRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteCoinsRequest(DeleteCoinRequest obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.DeleteCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > DeleteCoinsRequest");
            }
            return Json(res);
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
    }
}

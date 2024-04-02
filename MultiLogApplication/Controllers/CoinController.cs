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
                string contentPath = _configuration["StoragePath:paymentProof:Path"];
                string fileName = Guid.NewGuid().ToString();
                if (!Directory.Exists(wwwPath + contentPath))
                {
                    Directory.CreateDirectory(wwwPath + contentPath);
                }
                var extenstion = obj.File.FileName.Split(".").LastOrDefault();
                string docName = wwwPath + contentPath + "\\" + Path.GetFileName(fileName + "." + extenstion);
                using (FileStream stream = new FileStream(Path.Combine(wwwPath, contentPath, docName), FileMode.Create))
                {
                    obj.File.CopyTo(stream);
                }

                obj.UserId = _sessionUser;
                obj.SessionUser = _sessionUser;
                obj.ImageName = obj.File.FileName;
                obj.DocumentDetailId = fileName;
                obj.FileExtenstion = extenstion;
                obj.ImageSize = obj.File.Length.ToString();
                res = await _coinService.AddCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoinsRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> WithDrawCoinsRequest(DeleteCoinRequest obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.WithDrawCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > WithDrawCoinsRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddCoins(InsertCoinDetails obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.AddCoins(obj);
                HttpContext.Session.SetString("Coins", res.ReturnVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoins");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteCoins(InsertCoinDetails obj)
        {
            ReturnType<string> res = new ReturnType<string>();
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

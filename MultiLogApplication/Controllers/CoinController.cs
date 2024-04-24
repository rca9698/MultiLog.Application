using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.ActionFilter;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Service;
using System.Security.Claims;

namespace MultiLogApplication.Controllers
{
    public class CoinController : BaseController
    {
        private readonly ICoinService _coinService;
        private readonly ILogger<CoinController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CoinController(ICoinService coinService, ILogger<CoinController> logger
            , IHttpContextAccessor httpContextAccessor, IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
            : base(httpContextAccessor)
        {
            _coinService = coinService;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(string viewType)
        {
            return View("/Views/Coin/Index.cshtml", viewType);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> GetDepositCoinsRequest(DepositWithdrawCoinsRequest obj)
        {
            ReturnType<CoinsRequestModel> res = new ReturnType<CoinsRequestModel>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                obj.CoinType = 1;
                res = await _coinService.GetCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetDepositCoinsRequest");
            }
            return PartialView("~/Views/Coin/DepositCoinsRequests.cshtml", res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
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
        [ServiceFilter(typeof(AdminActionFilter))]
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
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                string BasePath = _hostingEnvironment.WebRootPath;
                //string wwwPath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:paymentProof:Path"];
                string fileName = Guid.NewGuid().ToString();
                string iconContentPath = BasePath + contentPath;
                if (!Directory.Exists(iconContentPath))
                {
                    Directory.CreateDirectory(iconContentPath);
                }
                var extenstion = obj.File.FileName.Split(".").LastOrDefault();
                string docName = iconContentPath + "\\" + Path.GetFileName(fileName + "." + extenstion);
                using (FileStream stream = new FileStream(docName, FileMode.Create))
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

        public async Task<IActionResult> WithDrawCoinsRequest(WithdrawCoinRequest obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _coinService.WithDrawCoinsRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > WithDrawCoinsRequest");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> AddCoins(UpdateCoinDetails obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.CoinType = 1;
                res = await _coinService.UpdateCoins(obj);
                //HttpContext.Session.SetString("Coins", res.ReturnVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoins");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> DeleteCoins(UpdateCoinDetails obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.CoinType = 0;
                res = await _coinService.UpdateCoins(obj);
                //HttpContext.Session.SetString("Coins", res.ReturnVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > DeleteCoins");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddCoinsToAccountRequest(UpdateCoinsToAccountRequestModel obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.UserId = _sessionUser;
                obj.SessionUser = _sessionUser;
                obj.CoinType = 0;
                res = await _coinService.UpdateCoinsToAccountRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddCoinsToAccountRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> WithDrawToAccountRequest(UpdateCoinsToAccountRequestModel obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.UserId = _sessionUser;
                obj.SessionUser = _sessionUser;
                obj.CoinType = 1;
                res = await _coinService.UpdateCoinsToAccountRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > WithDrawToAccountRequest");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> GetDepositCoinsToAccountRequest()
        {
            ReturnType<CoinsToAccountRequestModel> res = new ReturnType<CoinsToAccountRequestModel>();
            try
            {
                res = await _coinService.GetCoinsToAccountRequest(0, _sessionUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetDepositCoinsToAccountRequest");
            }
            return PartialView("~/Views/Coin/GetDepositCoinsToAccountRequest.cshtml", res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> GetWithdrawCoinsFromAccountRequest()
        {
            ReturnType<CoinsToAccountRequestModel> res = new ReturnType<CoinsToAccountRequestModel>();
            try
            {
                res = await _coinService.GetCoinsToAccountRequest(1, _sessionUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetWithdrawCoinsFromAccountRequest");
            }
            return PartialView("~/Views/Coin/GetWithdrawCoinsFromAccountRequest.cshtml", res);
        }

        public async Task<IActionResult> GetCoinsFromAccountRequestById(string CoinsRequestId, int CoinType)
        {
            ReturnType<CoinsToAccountRequestModel> res = new ReturnType<CoinsToAccountRequestModel>();
            try
            {
                res = await _coinService.GetCoinsToAccountRequest(CoinType, _sessionUser);
                res.ReturnVal = res.ReturnList.FirstOrDefault(x=>x.CoinsRequestId == CoinsRequestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > GetCoinsFromAccountRequestById");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> UpdateCoinsToAccount(UpdateCoinsToAccountModel obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.UpdateCoinsToAccount(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > UpdateCoinsToAccount");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> WithdrawCoinsFromAccount(UpdateCoinsToAccountModel obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.CoinType = 0;
                obj.SessionUser = _sessionUser;
                res = await _coinService.UpdateCoinsToAccount(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > WithdrawCoinsFromAccount");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> DeleteAccountRequestCoins(DeleteRequestCoinsModel obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            { 
                obj.SessionUser = _sessionUser;
                res = await _coinService.DeleteAccountRequestCoins(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > DeleteAccountRequestCoins");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> DeleteRequestCoins(DeleteRequestCoinsModel obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _coinService.DeleteRequestCoins(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > DeleteAccountRequestCoins");
            }
            return Json(res);
        }

    }
}

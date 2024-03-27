using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Coin;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Controllers
{
    public class BankAccountController : BaseController
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ILogger<BankAccountController> _logger;
        private readonly IConfiguration _configuration;
        public BankAccountController(IBankAccountService bankAccountService, ILogger<BankAccountController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor)
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> ViewPanel()
		{
			GetBankAccount bankAccount = new GetBankAccount();
            bankAccount.UserId = _sessionUser;
            bankAccount.SessionUser = _sessionUser;
			ReturnType<BankDetails> res = new ReturnType<BankDetails>();
			try
			{
				res = await _bankAccountService.GetBankAccounts(bankAccount);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Exception Occured at BankAccountController > ViewPanel");
			}
			return PartialView(res);
		}

		public async Task<IActionResult> AddBankAccount(AddBankAccount bankAccount)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                bankAccount.SessionUser = _sessionUser;
                bankAccount.UserId = _sessionUser;
                res = await _bankAccountService.AddBankAccount(bankAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > AddAccount");
            }
            return Json(res);
        }

        public async Task<IActionResult> GetBankAccounts(GetBankAccount bankAccount)
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                bankAccount.SessionUser = _sessionUser;
                res = await _bankAccountService.GetBankAccounts(bankAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > GetBankAccounts");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteBankAccount(DeleteBankAccount bankAccount)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                bankAccount.SessionUser = _sessionUser;
                res = await _bankAccountService.DeleteBankAccount(bankAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > DeleteBankAccount");
            }
            return Json(res);
        }

        public async Task<IActionResult> updateBankAccount(UpdateBankAccount bankAccount)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                bankAccount.SessionUser = _sessionUser;
                res = await _bankAccountService.updateBankAccount(bankAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > updateBankAccount");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddQRCode(InsertCoinRequest obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                string filePath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:QRPath:Path"];

                string pathDocument = Path.Combine(filePath, contentPath);

                if (!Directory.Exists(pathDocument))
                {
                    Directory.CreateDirectory(pathDocument);
                }
                string docName = Path.GetFileName(obj.File.FileName);
                using (FileStream stream = new FileStream(Path.Combine(pathDocument, docName), FileMode.Create))
                {
                    obj.File.CopyTo(stream);
                }

                return Json(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at CoinController > AddQRCode");
            }
            return Json(res);
        }

    }
}

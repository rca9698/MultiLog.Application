using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.ActionFilter;
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
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BankAccountController(IBankAccountService bankAccountService, ILogger<BankAccountController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : base(httpContextAccessor)
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string viewType)
        {
            return View("/Views/BankAccount/Index.cshtml", viewType);
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
        [ServiceFilter(typeof(UserActionFilter))]
        public async Task<IActionResult> ActiveBankAccounts()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                GetBankAccount obj = new GetBankAccount()
                {
                    UserId = _sessionUser,
                    SessionUser = _sessionUser,
                    IsActive = 1
                };
                res = await _bankAccountService.ActiveBankAccounts(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > ActiveBankAccounts");
            }
            return PartialView("~/Views/BankAccount/ActiveBankAccounts.cshtml",res);
        }

        public async Task<IActionResult> DeletedBankAccounts()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                GetBankAccount obj = new GetBankAccount()
                {
                    UserId = _sessionUser,
                    SessionUser = _sessionUser,
                    IsActive = 0
                };
                res = await _bankAccountService.DeletedBankAccounts(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > ActiveBankAccounts");
            }
            return PartialView("~/Views/BankAccount/DeletedBankAccounts.cshtml", res);
        }

        public async Task<IActionResult> BankAccountsHistory()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > BankAccountsHistory");
            }
            return PartialView("~/Views/BankAccount/BankAccountsHistory.cshtml", res);
        }

        public async Task<IActionResult> AddBankAccount(AddBankAccount bankAccount)
        {
            ReturnType<string> res = new ReturnType<string>();
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

        public async Task<IActionResult> SetDefaultBankAccount(long BankDetailID)
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                res = await _bankAccountService.SetDefaultBankAccount(_sessionUser, BankDetailID);
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
                bankAccount.IsActive = 1;
                bankAccount.UserId = _sessionUser;
                res = await _bankAccountService.GetBankAccounts(bankAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > GetBankAccounts");
            }
            return Json(res);
        }

        public async Task<IActionResult> GetBankUPIDetails()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                res = await _bankAccountService.GetBankUPIDetails();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > GetBankUPIDetails");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteBankAccount(DeleteBankAccount bankAccount)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                bankAccount.UserId = _sessionUser;
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
            ReturnType<string> res = new ReturnType<string>();
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

        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> AdminBankAccounts()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                res = await _bankAccountService.AdminBankAccounts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > AdminBankAccounts");
            }
            return PartialView("~/Views/BankAccount/AdminBankAccountDetails.cshtml", res);
        }
        
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> AddUpdateAdminBankAccount(AddAdminBankAccount obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.UserId = _sessionUser;
                obj.SessionUser = _sessionUser;
                res = await _bankAccountService.AddUpdateAdminBankAccount(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > AddUpdateAdminBankAccount");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> DeleteAdminBankAccount(DeleteAdminBankAccount obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _bankAccountService.DeleteAdminBankAccount(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > DeleteAdminBankAccount");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> SetDefaultAdminBankAccount(long BankDetailID)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                res = await _bankAccountService.SetDefaultAdminBankAccount(_sessionUser, BankDetailID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > SetDefaultAdminBankAccount");
            }
            return Json(res);
        }

        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> AdminUpiAccounts()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                res = await _bankAccountService.AdminUpiAccounts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > AdminUpiAccounts");
            }
            return PartialView("~/Views/BankAccount/AdminUpiDetails.cshtml", res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> AddUpdateAdminUpiAccount(AddUpdateAdminUpiAccount obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _bankAccountService.AddUpdateAdminUpiAccount(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > AddUpdateAdminUpiAccount");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> DeleteAdminUpiAccount(long UpiId)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                res = await _bankAccountService.DeleteAdminUpiAccount(_sessionUser, UpiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > DeleteAdminUpiAccount");
            }
            return Json(res);
        }
        [ServiceFilter(typeof(AdminActionFilter))]
        public async Task<IActionResult> SetDefaultAdminUpiAccount(long UpiId)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                res = await _bankAccountService.SetDefaultAdminUpiAccount(_sessionUser, UpiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > SetDefaultAdminUpiAccount");
            }
            return Json(res);
        }



        public async Task<IActionResult> AdminQRDetails()
        {
            ReturnType<BankDetails> res = new ReturnType<BankDetails>();
            try
            {
                res = await _bankAccountService.GetAdminQRCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at BankAccountController > AdminQRDetails");
            }
            return PartialView("~/Views/BankAccount/AdminQRDetails.cshtml", res);
        }

        public async Task<IActionResult> AddQRCode(AddUPIQRCode obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                string BasePath = _hostingEnvironment.WebRootPath;
                //string wwwPath = _configuration["StoragePath:BasePath:Path"];
                string contentPath = _configuration["StoragePath:PaymentPath:Path"];
                string fileName = "qascanner";
                string iconContentPath = BasePath + contentPath;
                if (!Directory.Exists(iconContentPath))
                {
                    Directory.CreateDirectory(iconContentPath);
                }
                var extenstion = obj.File.FileName.Split(".").LastOrDefault();
                string docName = iconContentPath + "\\" + Path.GetFileName(fileName + "." + extenstion);
                using (FileStream stream = new FileStream(Path.Combine(docName), FileMode.Create))
                {
                    obj.File.CopyTo(stream);
                }

                res = await _bankAccountService.AddAdminQRCode(_sessionUser,obj.UserName);

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

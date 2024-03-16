using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService,ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _accountService = accountService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AccountRequestList(GetAccounts obj)
        {
            ReturnType<AccountDetail> res = new ReturnType<AccountDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.AccountRequestList(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AccountRequestList");
            }
            return View(res);
        }

        public async Task<IActionResult> AccountList(GetAccounts account)
        {
            ReturnType<AccountDetail> res = new ReturnType<AccountDetail>();
            try
            {
                account.SessionUser = _sessionUser;
                res = await _accountService.GetAccounts(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AccountList");
            }
            return View(res);
        }

        public async Task<IActionResult> AddAccount(AddAccount account)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                account.SessionUser = _sessionUser;
                res = await _accountService.AddAccount(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AddAccount");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddAccountRequest(AddAccountRequest account)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                account.SessionUser = _sessionUser;
                res = await _accountService.AddAccountRequest(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AddAccountRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteAccount(DeleteAccount account)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                account.SessionUser = _sessionUser;
                res = await _accountService.DeleteAccount(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > DeleteAccount");
            }
            return Json(res);
        }

        
    }
}

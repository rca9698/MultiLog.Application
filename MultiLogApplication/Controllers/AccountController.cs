using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;

namespace MultiLogApplication.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IIDService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IIDService accountService,ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor,ILoginServices loginServices) : base(httpContextAccessor,loginServices)
        {
            _accountService = accountService;
            _logger = logger;
        }
        public IActionResult Index(string viewType)
        {
            return View("/Views/Account/Index.cshtml", viewType);
        }

        public async Task<IActionResult> AccountRequestList(GetIDs obj)
        {
            ReturnType<IDRequest> res = new ReturnType<IDRequest>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.IDRequestList(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AccountRequestList");
            }
            return PartialView(res);
        }

        public async Task<IActionResult> AccountRequestDetails(long id)
        {
            ReturnType<IDRequest> res = new ReturnType<IDRequest>();
            try
            {
                res = await _accountService.IDRequestDetails(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AccountRequestList");
            }
            return Json(res);
        }

        public async Task<IActionResult> AccountList(GetIDs obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.GetIDs(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AccountList");
            }
            return PartialView(res);
        }

        public async Task<IActionResult> AddAccount(AddAccount obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _accountService.AddID(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AddAccount");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddAccountRequest(AddIDRequest obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.UserId = _sessionUser;
                obj.SessionUser = _sessionUser;
                res = await _accountService.AddIDRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AddAccountRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteAccountRequest(DeleteIDRequest obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _accountService.DeleteIDRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > DeleteAccountRequest");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteAccount(DeleteID obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _accountService.DeleteID(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > DeleteAccount");
            }
            return Json(res);
        }

        public async Task<IActionResult> RejectedRequestLists(DeleteID obj)
        {
            ReturnType<string> res = new ReturnType<string>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _accountService.DeleteID(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > DeleteAccount");
            }
            return Json(res);
        }


        public async Task<IActionResult> ListChangeIDPassword(GetIDs obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.ListIDChangePassword(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > ListAccountChangePassword");
            }
            return PartialView(res);
        }

        public async Task<IActionResult> ListCloseIDRequest(GetIDs obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.ListIDCloseRequest(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > ListAccountCloseRequest");
            }
            return PartialView(res);
        }

        public async Task<IActionResult> AddChangeIDPassword(ChangeIDPasswordRequest obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.AddChangeIDPassword(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AddChangeAccountPassword");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddCloseID(CloseIDRequest obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                obj.UserId = _sessionUser;
                res = await _accountService.AddCloseID(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > AddCloseID");
            }
            return Json(res);
        }

        public async Task<IActionResult> ConfirmChangeIDPassword(ChangeIDPasswordRequest obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _accountService.ConfirmChangeIDPassword(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > ConfirmChangeIDPassword");
            }
            return Json(res);
        }

        public async Task<IActionResult> ConfirmCloseID(CloseIDRequest obj)
        {
            ReturnType<IDDetail> res = new ReturnType<IDDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _accountService.ConfirmCloseID(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at AccountController > ConfirmCloseID");
            }
            return Json(res);
        }

    }
}

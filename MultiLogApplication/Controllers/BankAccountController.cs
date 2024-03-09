using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Common;

namespace MultiLogApplication.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ILogger<BankAccountController> _logger;
        private readonly long _sessionUser;
        public BankAccountController(IBankAccountService bankAccountService, ILogger<BankAccountController> logger)
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddBankAccount(AddBankAccount bankAccount)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                bankAccount.SessionUser = _sessionUser;
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

    }
}

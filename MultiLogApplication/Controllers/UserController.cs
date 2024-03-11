using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.SiteDetails;
using MultiLogApplication.Models.User;

namespace MultiLogApplication.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewPanel()
        {
            ReturnType<UserDetail> res = new ReturnType<UserDetail>();
            try
            {
                GetUsers obj = new GetUsers();
                obj.SessionUser = _sessionUser;
                res = await _userService.GetUsers(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at UserController > Getsites");
            }
            return View(res.ReturnList);
        }

        public async Task<IActionResult> GetUsers(GetUsers obj)
        {
            ReturnType<UserDetail> res = new ReturnType<UserDetail>();
            try
            {


                obj.SessionUser = _sessionUser;
                res = await _userService.GetUsers(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at UserController > Getsites");
            }
            return Json(res);
        }

        public async Task<IActionResult> AddUser(AddUser obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _userService.AddUser(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at UserController > AddUser");
            }
            return Json(res);
        }

        public async Task<IActionResult> UpdateUser(UpdateUser obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _userService.UpdateUser(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at UserController > UpdateUser");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteUser(DeleteUser obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _userService.DeleteUser(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at UserController > DeleteUser");
            }
            return Json(res);
        }

    }
}

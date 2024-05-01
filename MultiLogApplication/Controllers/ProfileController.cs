using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Profile;

namespace MultiLogApplication.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(IHttpContextAccessor httpContextAccessor, ILoginServices loginServices,IProfileService profileService, ILogger<ProfileController> logger)
            :base(httpContextAccessor,loginServices)
        {
            _profileService = profileService;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordModel passwordModel)
        {
            ReturnType<string> returnType = new ReturnType<string>();

            try
            {
                passwordModel.MobileNumber = _userNumber;
                passwordModel.UserId = _sessionUser;
                returnType =await _profileService.ChangePassword(passwordModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at ProfileController > ChangePassword");
            }

            return Json(returnType);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.BankAccount;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.NotificationDetails;

namespace MultiLogApplication.Controllers
{
    public class NotificationController : BaseController
    {

        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewPanel()
        {
            return View("~/Views/Notification/ViewPanel.cshtml");
        }

        public async Task<IActionResult> InsertNotification(InsertNotification obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _notificationService.InsertNotification(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at NotificationController > InsertNotification");
            }
            return Json(res);
        }

        public async Task<IActionResult> UpdateNotification(UpdateNotification obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _notificationService.UpdateNotification(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at NotificationController > UpdateNotification");
            }
            return Json(res);
        }

        public async Task<IActionResult> DeleteNotification(DeleteNotification obj)
        {
            ReturnType<bool> res = new ReturnType<bool>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _notificationService.DeleteNotification(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at NotificationController > DeleteNotification");
            }
            return Json(res);
        }

        public async Task<IActionResult> GetNotifications(GetNotification obj)
        {
            ReturnType<NotificationDetail> res = new ReturnType<NotificationDetail>();
            try
            {
                obj.SessionUser = _sessionUser;
                res = await _notificationService.GetNotifications(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured at NotificationController > GetNotifications");
            }
            return Json(res);
        }

    }
}

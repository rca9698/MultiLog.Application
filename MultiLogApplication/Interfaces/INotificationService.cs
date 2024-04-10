using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.NotificationDetails;

namespace MultiLogApplication.Interfaces
{
    public interface INotificationService
    {
        Task<ReturnType<string>> InsertNotification(InsertNotification entity);
        Task<ReturnType<string>> UpdateNotification(UpdateNotification entity);
        Task<ReturnType<string>> DeleteNotification(DeleteNotification entity);
        Task<ReturnType<NotificationDetail>> GetNotifications(GetNotification entity);
    }
}

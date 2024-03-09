using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.NotificationDetails;

namespace MultiLogApplication.Interfaces
{
    public interface INotificationService
    {
        Task<ReturnType<bool>> InsertNotification(InsertNotification entity);
        Task<ReturnType<bool>> UpdateNotification(UpdateNotification entity);
        Task<ReturnType<bool>> DeleteNotification(DeleteNotification entity);
        Task<ReturnType<NotificationDetail>> GetNotifications(GetNotification entity);
    }
}

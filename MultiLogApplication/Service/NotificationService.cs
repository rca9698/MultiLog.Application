using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.NotificationDetails;

namespace MultiLogApplication.Service
{
    public class NotificationService : BaseService, INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(HttpClient client, IConfiguration configuration, ILogger<NotificationService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<string>> DeleteNotification(DeleteNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/DeleteNotification", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<NotificationDetail>> GetNotifications(GetNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/GetNotifications", details);
            return await response.ReadContentAs<ReturnType<NotificationDetail>>();
        }

        public async Task<ReturnType<string>> InsertNotification(InsertNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/InsertNotification", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> UpdateNotification(UpdateNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/UpdateNotification", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }
    }
}

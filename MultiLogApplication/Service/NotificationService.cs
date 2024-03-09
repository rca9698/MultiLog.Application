using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.NotificationDetails;

namespace MultiLogApplication.Service
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public NotificationService(HttpClient client, IConfiguration configuration, ILogger<NotificationService> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ReturnType<bool>> DeleteNotification(DeleteNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/DeleteNotification", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<NotificationDetail>> GetNotifications(GetNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/GetNotifications", details);
            return await response.ReadContentAs<ReturnType<NotificationDetail>>();
        }

        public async Task<ReturnType<bool>> InsertNotification(InsertNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/InsertNotification", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> UpdateNotification(UpdateNotification details)
        {
            var response = await _client.PostAsJsonAsync($"api/Notification/UpdateNotification", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }
    }
}

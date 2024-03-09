namespace MultiLogApplication.Models.NotificationDetails
{
    public class UpdateNotification
    {
        public long NotificationId { get; set; }
        public string NotificationDescription { get; set; }
        public long SessionUser { get; set; }
    }
}

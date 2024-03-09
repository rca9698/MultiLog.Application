namespace MultiLogApplication.Models.NotificationDetails
{
    public class InsertNotification
    {
        public long NotificationId { get; set; }
        public string NotificationDescription { get; set; }
        public long SessionUser { get; set; }
    }
}

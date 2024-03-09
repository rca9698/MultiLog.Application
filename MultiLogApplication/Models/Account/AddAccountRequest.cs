namespace MultiLogApplication.Models.Account
{
    public class AddAccountRequest
    {
        public long UserId { get; set; }
        public int SiteId { get; set; }
        public long SessionUser { get; set; }
    }
}

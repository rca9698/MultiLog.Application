namespace MultiLogApplication.Models.Account
{
    public class AddIDRequest
    {
        public long UserId { get; set; }
        public int SiteId { get; set; }
        public string UserName { get; set; }
        public long SessionUser { get; set; }
    }
}

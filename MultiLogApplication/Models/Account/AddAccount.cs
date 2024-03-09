namespace MultiLogApplication.Models.Account
{
    public class AddAccount
    {
        public long UserId { get; set; }
        public int SiteId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long SessionUser { get; set; }
    }
}

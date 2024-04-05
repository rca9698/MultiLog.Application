namespace MultiLogApplication.Models.Account
{
    public class AccountDetail
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserNumber { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteURL { get; set; }
        public string SiteIcon { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}

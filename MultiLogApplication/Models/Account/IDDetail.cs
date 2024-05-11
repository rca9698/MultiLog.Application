namespace MultiLogApplication.Models.Account
{
    public class IDDetail
    {
        public long ReqId { get; set; }
        public long UserId { get; set; }
        public long AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserNumber { get; set; }
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteURL { get; set; }
        public string DocumentDetailId { get; set; }
        public string FileExtenstion { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}

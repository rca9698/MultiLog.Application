namespace MultiLogApplication.Models.SiteDetails
{
    public class UpdateSite
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteURL { get; set; }
        public string SiteImage { get; set; }
        public long SessionUser { get; set; }
    }
}

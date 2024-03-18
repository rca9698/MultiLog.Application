namespace MultiLogApplication.Models.SiteDetails
{
    public class AddSite
    {
        public string SiteName { get; set; }
        public string SiteURL { get; set; }
        public string DocumentDetailId { get; set; }
        public string ImageName { get; set; }
        public string ImageSize { get; set; }
        public string FileExtenstion { get; set; }
        public long SessionUser { get; set; }
        public IFormFile File { get; set; }
    }
}

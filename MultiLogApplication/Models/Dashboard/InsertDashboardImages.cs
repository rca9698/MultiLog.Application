namespace MultiLogApplication.Models.Dashboard
{
    public class InsertDashboardImages
    {
        public string DocumentDetailId { get; set; }
        public string FileExtenstion { get; set; }
        public string DisplayDate { get; set; }
        public IFormFile File { get; set; }
    }
}

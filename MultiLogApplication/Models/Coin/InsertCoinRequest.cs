namespace MultiLogApplication.Models.Coin
{
    public class InsertCoinRequest
    {
        public int Coins { get; set; }
        public string DocumentDetailId { get; set; }
        public IFormFile File { get; set; }
        public string ImageName { get; set; }
        public string ImageSize { get; set; }
        public string FileExtenstion { get; set; }
        public long SessionUser { get; set; }
        public long UserId { get; set; }
    }
}

namespace MultiLogApplication.Models.Coin
{
    public class DeleteCoinsModel
    {
        public long UserId { get; set; }
        public long Coins { get; set; }
        public int CoinType { get; set; }
        public string CoinsRequestId { get; set; }
        public string DocumentDetailId { get; set; }
        public string ImageName { get; set; }
        public string ImageSize { get; set; }
        public string FileExtenstion { get; set; }
        public long SessionUser { get; set; }
        public IFormFile File { get; set; }
    }
}

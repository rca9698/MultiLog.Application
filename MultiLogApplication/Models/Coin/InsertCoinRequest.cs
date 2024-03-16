namespace MultiLogApplication.Models.Coin
{
    public class InsertCoinRequest
    {
        public int Coins { get; set; }
        public IFormFile File { get; set; }
        public long SessionUser { get; set; }
        public long UserId { get; set; }
    }
}

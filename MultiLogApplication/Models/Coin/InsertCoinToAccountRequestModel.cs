namespace MultiLogApplication.Models.Coin
{
    public class InsertCoinToAccountRequestModel
    {
        public long SiteId { get; set; }
        public long UserId { get; set; }
        public long Coins { get; set; }
        public long SessionUser { get; set; }
    }
}

namespace MultiLogApplication.Models.Coin
{
    public class UpdateCoinsToAccountRequestModel
    {
        public long SiteId { get; set; }
        public long UserId { get; set; }
        public long Coins { get; set; }
        public int CoinType { get; set; }
        public long SessionUser { get; set; }
    }
}

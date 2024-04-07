namespace MultiLogApplication.Models.Coin
{
    public class UpdateCoinsToAccountModel
    {
        public string CoinsRequestId { get; set; }
        public long UserId { get; set; }
        public int SiteId { get; set; }
        public long Coins { get; set; }
        public int CoinType { get; set; }
        public long SessionUser { get; set; }

    }
}

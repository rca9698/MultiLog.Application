namespace MultiLogApplication.Models.Coin
{
    public class UpdateCoinDetails
    {
        public long SessionUser { get; set; }
        public long UserId { get; set; }
        public long Coins { get; set; }
        public int CoinType { get; set; }
        public string CoinsRequestId { get; set; }
    }
}

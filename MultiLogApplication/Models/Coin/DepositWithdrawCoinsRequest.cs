namespace MultiLogApplication.Models.Coin
{
    public class DepositWithdrawCoinsRequest
    {
        public int CoinType { get; set; }
        public long SessionUser { get; set; }
    }
}

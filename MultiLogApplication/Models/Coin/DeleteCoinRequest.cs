namespace MultiLogApplication.Models.Coin
{
    public class DeleteCoinRequest
    {
        public int Coins { get; set; }
        public long SessionUser { get; set; }
        public long UserId { get; set; }
        public long BankId { get; set; }
    }
}

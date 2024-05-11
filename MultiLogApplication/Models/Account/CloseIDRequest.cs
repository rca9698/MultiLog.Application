namespace MultiLogApplication.Models.Account
{
    public class CloseIDRequest
    {
        public long UserId { get; set; }
        public long AccountId { get; set; }
        public long SessionUser { get; set; }
    }
}

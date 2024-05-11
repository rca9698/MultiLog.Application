namespace MultiLogApplication.Models.Account
{
    public class ChangeIDPasswordRequest
    {
        public long UserId { get; set; }
        public long AccountId { get; set; }
        public long SessionUser { get; set; }
        public string Password { get; set; }
    }
}

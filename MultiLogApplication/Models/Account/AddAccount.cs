namespace MultiLogApplication.Models.Account
{
    public class AddAccount
    {
        public long AccountRequestId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long SessionUser { get; set; }
    }
}

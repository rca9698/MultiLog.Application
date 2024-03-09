namespace MultiLogApplication.Models.BankAccount
{
    public class BankDetails
    {
        public long BankId { get; set; }
        public long UserId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
    }
}

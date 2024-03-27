namespace MultiLogApplication.Models.BankAccount
{
    public class BankDetails
    {
        public long BankAccountDetailID { get; set; }
        public long UserId { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
    }
}

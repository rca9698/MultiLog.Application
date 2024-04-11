namespace MultiLogApplication.Models.BankAccount
{
    public class AddAdminBankAccount
    {
        public long UserId { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string UpiId { get; set; }
        public long SessionUser { get; set; }
    }
}

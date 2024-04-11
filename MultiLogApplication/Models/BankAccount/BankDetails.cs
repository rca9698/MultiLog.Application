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
        public bool IsDefault { get; set; }
        public string UpiId { get; set; }
        public string QrPath { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}

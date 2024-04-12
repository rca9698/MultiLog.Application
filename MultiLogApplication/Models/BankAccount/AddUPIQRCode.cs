namespace MultiLogApplication.Models.BankAccount
{
    public class AddUPIQRCode
    {
        public IFormFile File { get; set; }
        public long UpiId { get; set; }
        public string UserName { get; set; }
    }
}

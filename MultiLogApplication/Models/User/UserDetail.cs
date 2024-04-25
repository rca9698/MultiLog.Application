namespace MultiLogApplication.Models.User
{
    public class UserDetail
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserNumber { get; set; }
        public string EmailId { get; set; }
        public string Claims { get; set; }
        public string Coins { get; set; }
        public string Password { get; set; }
        public string Otp { get; set; }
        public long SessionUser { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}

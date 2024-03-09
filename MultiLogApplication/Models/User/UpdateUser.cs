namespace MultiLogApplication.Models.User
{
    public class UpdateUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long SessionUser { get; set; }
    }
}

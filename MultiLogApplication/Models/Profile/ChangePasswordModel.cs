namespace MultiLogApplication.Models.Profile
{
    public class ChangePasswordModel
    {
        public long UserId { get; set; }
        public string MobileNumber { get; set; }
        public string CurrentPassword { get; set; }
        public string ChangePassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

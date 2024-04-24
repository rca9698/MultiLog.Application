using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;
using MultiLogApplication.Models.LoginModel;
using MultiLogApplication.Models.User;

namespace MultiLogApplication.Interfaces
{
    public interface ILoginServices
    {
        public Task<ReturnType<UserDetail>> LoginCred(LoginDetails details);
        public Task<ReturnType<bool>> SignupCred(SignUpDetails details);
        public Task<ReturnType<string>> SendOTP(string MobileNumber);
    }
}

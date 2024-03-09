using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Common.LoginSignup;

namespace MultiLogApplication.Interfaces
{
    public interface ILoginServices
    {
        public Task<ReturnType<bool>> LoginCred(LoginDetails details);
        public Task<ReturnType<bool>> SignupCred(SignUpDetails details);
    }
}

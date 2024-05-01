using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Profile;

namespace MultiLogApplication.Interfaces
{
    public interface IProfileService
    {
        Task<ReturnType<string>> ChangePassword(ChangePasswordModel passwordModel);
    }
}

using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.User;

namespace MultiLogApplication.Interfaces
{
    public interface IUserService
    {
        Task<ReturnType<UserDetail>> GetUsers(GetUsers details);
        Task<ReturnType<string>> AddUser(AddUser details);
        Task<ReturnType<string>> DeleteUser(DeleteUser details);
        Task<ReturnType<string>> UpdateUser(UpdateUser details);
        Task<ReturnType<UserDetail>> GetUserById(GetUserById details);
    }
}

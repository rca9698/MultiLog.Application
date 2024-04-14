using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.User;

namespace MultiLogApplication.Interfaces
{
    public interface IUserService
    {
        Task<ReturnType<UserDetail>> GetUsers(GetUsers details);
        Task<ReturnType<bool>> AddUser(AddUser details);
        Task<ReturnType<bool>> DeleteUser(DeleteUser details);
        Task<ReturnType<bool>> UpdateUser(UpdateUser details);
        Task<ReturnType<UserDetail>> GetUserById(GetUserById details);
    }
}

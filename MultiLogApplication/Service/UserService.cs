using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.DropDown;
using MultiLogApplication.Models.User;

namespace MultiLogApplication.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;
        public UserService(HttpClient client, IConfiguration configuration, ILogger<UserService> logger, ITokenService tokenService) : base(client, configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<string>> AddUser(AddUser details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/AddUser", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<string>> DeleteUser(DeleteUser details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/DeleteUser", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<UserDetail>> GetUsers(GetUsers details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/GetUsers", details);
            return await response.ReadContentAs<ReturnType<UserDetail>>();
        }

        public async Task<ReturnType<string>> UpdateUser(UpdateUser details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/UpdateUser", details);
            return await response.ReadContentAs<ReturnType<string>>();
        }

        public async Task<ReturnType<UserDetail>> GetUserById(GetUserById details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/GetUserById", details);
            return await response.ReadContentAs<ReturnType<UserDetail>>();
        }
    }
}

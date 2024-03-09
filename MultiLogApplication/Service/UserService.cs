using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.DropDown;
using MultiLogApplication.Models.User;

namespace MultiLogApplication.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public UserService(HttpClient client, IConfiguration configuration, ILogger<UserService> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<ReturnType<bool>> AddUser(AddUser details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/AddUser", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<bool>> DeleteUser(DeleteUser details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/DeleteUser", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }

        public async Task<ReturnType<UserDetail>> GetUsers(GetUsers details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/GetUsers", details);
            return await response.ReadContentAs<ReturnType<UserDetail>>();
        }

        public async Task<ReturnType<bool>> UpdateUser(UpdateUser details)
        {
            var response = await _client.PostAsJsonAsync($"api/User/UpdateUser", details);
            return await response.ReadContentAs<ReturnType<bool>>();
        }
    }
}

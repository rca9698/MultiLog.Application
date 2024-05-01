using MultiLogApplication.Extensions;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Models.Account;
using MultiLogApplication.Models.Common;
using MultiLogApplication.Models.Profile;

namespace MultiLogApplication.Service
{
    public class ProfileService : BaseService, IProfileService
    {
        ILogger<ProfileService> _logger;
        public ProfileService(HttpClient client, IConfiguration configuration, ILogger<ProfileService> logger, ITokenService tokenService)
            :base(client,configuration,tokenService)
        {
            _logger = logger;
        }

        public async Task<ReturnType<string>> ChangePassword(ChangePasswordModel passwordModel)
        {
            var response = await _client.PostAsJsonAsync($"api/Profile/ChangePassword", passwordModel);
            return await response.ReadContentAs<ReturnType<string>>();
        }
    }
}

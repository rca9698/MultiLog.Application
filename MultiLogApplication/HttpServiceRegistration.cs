using MultiLogApplication.ActionFilter;
using MultiLogApplication.Interfaces;
using MultiLogApplication.Service;

namespace MultiLogApplication
{
    public static class HttpServiceRegistration
    {
        public static void AddHttpServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<AdminActionFilter>();
            services.AddScoped<UserActionFilter>();
            services.AddScoped<SesionActiveFilter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddHttpClient<ILoginServices, LoginServices>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<IIDService, IDService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<IBankAccountService, BankAccountService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<ICoinService, CoinService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<INotificationService, NotificationService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<ISiteService, SiteService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<IUserService, UserService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<IPassbookService, PassbookService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<IHomeService, HomeService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            services.AddHttpClient<IProfileService, ProfileService>(c => c.BaseAddress = new Uri(config["ApiConfigs:MultilogAPI:Uri"]));
            
        }
    }
}

using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<ITest, Test>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

    }
}

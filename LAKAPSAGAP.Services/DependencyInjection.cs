using Microsoft.AspNetCore.Components.Authorization;
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
            services.AddScoped<IUserAttachmentRepository, UserAttachmentRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            services.AddScoped<AuthRepository>();
            services.AddScoped<UserAttachmentRepository>();
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
           // services.AddScoped<AuthenticationStateProvider>();
            services.AddScoped<HttpContextAccessor>();
            return services;
        }

    }
}

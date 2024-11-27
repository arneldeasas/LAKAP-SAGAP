using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services;

public static class DependencyInjection
{

    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserAttachmentRepository, UserAttachmentRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IUoMRepository, UoMRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRackRepository, RackRepository>();
        services.AddScoped<IStockItemRepository, StockItemRepository>();

        services.AddScoped<AuthRepository>();
        services.AddScoped<UserAttachmentRepository>();
        //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
        // services.AddScoped<AuthenticationStateProvider>();
        services.AddScoped<HttpContextAccessor>();
        return services;
    }

}

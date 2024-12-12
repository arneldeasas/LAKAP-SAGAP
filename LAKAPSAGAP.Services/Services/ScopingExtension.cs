using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services.Services;

public static class ScopingExtension
{
	public static IServiceCollection AddAppScopes(this IServiceCollection services)
	{
		services.AddScoped<IAuthRepository, AuthRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IUserAttachmentRepository, UserAttachmentRepository>();
		services.AddScoped<IWarehouseRepository, WarehouseRepository>();
		services.AddScoped<IUoMRepository, UoMRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IReliefReceivedRepository, ReliefReceivedRepository>();
		services.AddScoped<IRackRepository, RackRepository>();
		services.AddScoped<IStockItemRepository, StockItemRepository>();
		services.AddScoped<IReliefRequestRepository, ReliefRequestRepository>();
		services.AddScoped<IKittingRepository, KittingRepository>();
		services.AddScoped<IPackedReliefKitRepository, PackedReliefKitRepository>();

		services.AddScoped<AuthRepository>();
		services.AddScoped<UserAttachmentRepository>();
		//services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
		// services.AddScoped<AuthenticationStateProvider>();
		services.AddScoped<HttpContextAccessor>();

		return services;
	}
}

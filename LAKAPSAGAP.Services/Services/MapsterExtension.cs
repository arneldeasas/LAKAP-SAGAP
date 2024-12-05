using Mapster;

namespace LAKAPSAGAP.Services.Services;

public static class MapsterExtension
{
	public static IServiceCollection AddMapsterService(this IServiceCollection services)
	{
		services.AddMapster();
		return services;
	}
}

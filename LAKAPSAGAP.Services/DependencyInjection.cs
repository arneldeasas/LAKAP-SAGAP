using LAKAPSAGAP.Services.Services;

namespace LAKAPSAGAP.Services;

public static class DependencyInjection
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services
			.AddHostedService<DatabaseMigrationService>()
			.AddCustomIdentity()
			.AddAppScopes()
			.AddCookie();
	}
}

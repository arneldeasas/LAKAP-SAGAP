namespace LAKAPSAGAP.Services.Services;

public static class CookieExtension
{
	public static IServiceCollection AddCookie(this IServiceCollection services)
	{
		return services.ConfigureApplicationCookie(options =>
		{
			// Cookie settings
			options.Cookie.HttpOnly = true;
			options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
			options.LoginPath = "/account/login";
			options.AccessDeniedPath = "/AccessDenied";
			options.SlidingExpiration = true;
		});
	}
}

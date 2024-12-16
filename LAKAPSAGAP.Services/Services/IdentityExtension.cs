namespace LAKAPSAGAP.Services.Services;

public static class IdentityExtension
{
	public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
	{
		services.AddIdentity<UserAuth, IdentityRole>(options =>
		{
			// Password settings.
			options.Password.RequireDigit = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequiredLength = 1;
			options.Password.RequiredUniqueChars = 0;

			// Lockout settings.
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.AllowedForNewUsers = true;

			// User settings.
			options.User.AllowedUserNameCharacters =
			"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
			options.User.RequireUniqueEmail = false;
			
		})
		.AddEntityFrameworkStores<MyDbContext>()
		.AddUserManager<UserManager<UserAuth>>()
		.AddDefaultTokenProviders();

		return services;
	}
}

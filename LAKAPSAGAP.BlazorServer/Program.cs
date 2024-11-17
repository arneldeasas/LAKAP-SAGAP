using LAKAPSAGAP.Services;
using LAKAPSAGAP.Services.Core.API;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vite.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddViteServices();

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddAuthorization();
builder.Services.AddAntiforgery();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddServices();
//builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

#if DEBUG
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endif

builder.Services.AddIdentity<UserAuth, IdentityRole>(options =>
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

builder.Services.ConfigureApplicationCookie(options =>
{
	// Cookie settings
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
	options.LoginPath = "/login";
	options.AccessDeniedPath = "/AccessDenied";
	options.SlidingExpiration = true;
});

builder.Services.AddHttpClient("API", client =>
{
	client.BaseAddress = new Uri("https://localhost:7224/");
});

var app = builder.Build();

// Ensure authentication and authorization middleware are used

app.MapEndpoints();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
//app.UseAuthentication(); // Ensure authentication middleware is used
//app.UseAuthorization();  // Ensure authorization middleware is used

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] {
		new { Name = "Barangay Representative", NormalizedName = "BARANGAY_REPRESENTATIVE" },
		new { Name = "CSWD Office Head", NormalizedName = "CSWD_OFFICE_HEAD" },
		new { Name = "CSWD Administration Staff", NormalizedName = "CSWD_ADMINISTRATION_STAFF" }
	};

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role.NormalizedName)) // Check if the role exists
		{
			var identityRole = new IdentityRole
			{
				Name = role.Name,
				NormalizedName = role.NormalizedName
			};

			await roleManager.CreateAsync(identityRole); // Create the role if it doesn't exist
		}
	}
}

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run(); // Ensure the application runs asynchronously

if (app.Environment.IsDevelopment())
{
	if (bool.Parse(builder.Configuration["Vite:Server:Enabled"] ?? string.Empty))
	{
		// Proxies requests for css and js to 
		// the Vite development server for HMR.
		//app.UseViteDevelopmentServer(true);
	}
}

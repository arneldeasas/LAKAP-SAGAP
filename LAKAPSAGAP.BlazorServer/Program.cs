using LAKAPSAGAP.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using LAKAPSAGAP.Services.Core.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddServices();
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(o =>
{
	o.DefaultScheme = IdentityConstants.ApplicationScheme;
	o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
});


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

	options.LoginPath = "/Login";
	options.AccessDeniedPath = "/AccessDenied";
	options.SlidingExpiration = true;
});


builder.Services.AddHttpClient("API", client =>
{
	client.BaseAddress = new Uri("https://localhost:7224/");
});

var app = builder.Build();
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
//app.UseAuthorization(); // Ensure authorization middleware is used



using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "REPRESENTATIVE", "OFFICE HEAD", "ADMIN" };
	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role)) // Check if the role exists
		{
			await roleManager.CreateAsync(new IdentityRole(role)); // Create the role if it doesn't exist
		}
	}
}
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run(); // Ensure the application runs asynchronously

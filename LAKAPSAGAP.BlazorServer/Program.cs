using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LAKAPSAGAP.Services;
using LAKAPSAGAP.BlazorServer;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddAuthorization(options =>
{
	//options.AddPolicy("officehead", policy => policy.RequireRole(["CSWD OFFICE HEAD", "CSWD Office Head"]));
	//options.AddPolicy("officehead/admin", policy => policy.RequireRole(["CSWD OFFICE HEAD","CSWD ADMINISTRATION STAFF"]));
	options.AddPolicy("officehead", policy => policy.RequireRole(["CSWD ADMINISTRATION STAFF"]));
	options.AddPolicy("officehead/admin", policy => policy.RequireRole(["CSWD OFFICE HEAD", "CSWD ADMINISTRATION STAFF", "CSWD Administration Staff"]));
	options.AddPolicy("barangayrep", policy => policy.RequireRole("BARANGAY REPRESENTATIVE"));
});
builder.Services.AddAntiforgery();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddBlazoredLocalStorage();

// Configure DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<CustomAuthenticationStateProvider>();

#if DEBUG
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endif

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
app.UseAuthorization();  // Ensure authorization middleware is used

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run(); // Ensure the application runs asynchronously
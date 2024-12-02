using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LAKAPSAGAP.Services;
using LAKAPSAGAP.BlazorServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddAuthorization();
builder.Services.AddAntiforgery();
builder.Services.AddCascadingAuthenticationState();


// Configure DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddServices();
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
//app.UseAuthorization();  // Ensure authorization middleware is used

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run(); // Ensure the application runs asynchronously
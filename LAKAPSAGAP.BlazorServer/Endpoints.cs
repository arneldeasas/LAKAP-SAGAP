using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LAKAPSAGAP.Services.Core.API
{
    public static class ApiEndpoints
    {
       
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapPost("/api/account/login", async (AuthRepository authRepository, HttpRequest request) =>
            {
                try
                {
					var data = await request.ReadFromJsonAsync<LoginViewModel>();
                    var ok =  await authRepository.Authenticate(data);
                   // customAuthenticationStateProvider.MarkUserAsAuthenticated(name);
                    if(ok)return Results.Ok(new {Message = "login success.",Data=data});
                    return Results.Json(new {message="login failed."},statusCode:401);
				}
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return Results.Json(new { message = "An unexpected error occured." }, statusCode: 500);
				}
            });
        }
    }
}

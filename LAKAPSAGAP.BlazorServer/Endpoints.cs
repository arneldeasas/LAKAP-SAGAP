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
                    var isSuccess =  await authRepository.Authenticate(data);

                    if(isSuccess)return Results.Ok(new {message = "login success."});
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

using LAKAPSAGAP.Services.Core;
using System.Text.Json;

namespace LAKAPSAGAP.BlazorServer;

public static class ApiEndpoints
{
	public static void MapEndpoints(this WebApplication app)
	{
		app.MapPost("/api/account/login", async (AuthRepository authRepository, HttpRequest request) =>
		{
			try
			{
				// Deserialize the request body into a LoginViewModel
				var data = await request.ReadFromJsonAsync<LoginViewModel>();

				// Validate the input data
				if (data == null || string.IsNullOrEmpty(data.Username) || string.IsNullOrEmpty(data.Password))
				{
					return Results.BadRequest(new { message = "Invalid input. Username and password are required." });
				}

				// Perform authentication
				var isAuthenticated = await authRepository.Authenticate(data);

				if (isAuthenticated)
				{
					return Results.Json(new { message = "Login successful.", data = new { data.Username } }, statusCode: 200);
				}
				else
				{
					return Results.Json(new { message = "Invalid username or password." }, statusCode: 401);
				}
			}
			catch (JsonException ex)
			{
				// Handle JSON parsing errors specifically
				Console.WriteLine($"JSON Parsing Error: {ex.Message}");
				return Results.Json(new { message = "Invalid JSON format." }, statusCode: 400);
			}
			catch (Exception ex)
			{
				// Handle unexpected errors
				Console.WriteLine($"Unexpected Error: {ex.Message}");
				return Results.Json(new { message = "An unexpected error occurred." }, statusCode: 500);
			}
		});
	}
}
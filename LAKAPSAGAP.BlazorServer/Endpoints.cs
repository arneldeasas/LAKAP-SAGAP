using LAKAPSAGAP.Services.Core;
using System.Text.Json;

namespace LAKAPSAGAP.BlazorServer;

public static class ApiEndpoints
{
	public static void MapEndpoints(this WebApplication app)
	{
		app.MapPost("/api/account/login", async (AuthRepository authRepository, HttpRequest request) =>
		{
			
		});
	}
}
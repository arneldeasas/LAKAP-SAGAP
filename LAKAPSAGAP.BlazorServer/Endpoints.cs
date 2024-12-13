using LAKAPSAGAP.Services.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace LAKAPSAGAP.BlazorServer;

public static class ApiEndpoints
{
	public static void MapEndpoints(this WebApplication app)
	{
		app.MapPost("/api/account/logout", async (SignInManager<UserAuth> _signInManager) =>
		{
			await _signInManager.SignOutAsync();

			return Results.Ok();
		});
	}
}
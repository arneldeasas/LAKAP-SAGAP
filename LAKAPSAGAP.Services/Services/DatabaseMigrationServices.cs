using Microsoft.Extensions.Hosting;

namespace LAKAPSAGAP.Services.Services;

public class DatabaseMigrationService : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public DatabaseMigrationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
		await context.Database.MigrateAsync(cancellationToken);
		Console.WriteLine("Database created and/or migrations applied successfully.");
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}


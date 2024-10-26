namespace LAKAPSAGAP.Services
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<ITest, Test>();

            return services;
        }

    }
}

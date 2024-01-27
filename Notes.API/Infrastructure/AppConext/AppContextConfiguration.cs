using Notes.Infrastructure;

namespace Notes.API.Infrastructure.AppConext
{
    public static class AppContextConfiguration
    {
        public static IServiceCollection AddAppContext(this IServiceCollection services)
        {
            services.AddScoped<IAppContext, AppContext>();
            services.AddScoped<AppContextMiddleware>();

            return services;
        }
    }
}

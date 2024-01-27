using Microsoft.EntityFrameworkCore;
using Notes.API.Infrastructure.AppConext;
using Notes.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Notes.API.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppContext();

            AddDbContext(services, configuration);

            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            static void ConfigureSqlOptions(NpgsqlDbContextOptionsBuilder options)
            {
                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
                options.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
            }

            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Notes"), ConfigureSqlOptions);
            });
        }
    }
}

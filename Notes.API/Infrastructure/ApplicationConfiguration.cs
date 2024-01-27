using Microsoft.EntityFrameworkCore;
using Notes.API.Infrastructure.AppConext;
using Notes.Infrastructure;
using Notes.Infrastructure.TagResolver;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Notes.API.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppContext();

            AddDbContext(services, configuration);
            AddTagResolvers(services);
            AddModules(services);

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

        private static void AddTagResolvers(IServiceCollection services)
        {
            var catalog = new DefaultTagResolverCatalog();
            catalog.Register(new EmailTagResolver());
            catalog.Register(new PhoneTagResolver());

            services.AddSingleton<ITagResolverCatalog>(catalog);
            services.AddSingleton<ITagGeneratorService, DefaultTagGeneratorService>();
        }

        private static void AddModules(IServiceCollection services)
        {
            Queries.Bootstrap.Register(services);
            Commands.Bootstrap.Register(services);
        }
    }
}

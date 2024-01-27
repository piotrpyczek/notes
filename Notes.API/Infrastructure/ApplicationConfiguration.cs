using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notes.API.Infrastructure.AppConext;
using Notes.Infrastructure;
using Notes.Infrastructure.Configuration;
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
            AddAuthentication(services, configuration);

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

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var authenticationOptions = configuration.GetSection(AuthenticationOptions.Section).Get<AuthenticationOptions>()!;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.JwtKey))
                    };
                });
        }
    }
}

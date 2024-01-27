
using Notes.Infrastructure;
using Notes.Infrastructure.TagResolver;

namespace Notes.API.Infrastructure.Database
{
    public static class DatabaseAppBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<NotesDbContextMigration>>();
            var catalog = scope.ServiceProvider.GetRequiredService<ITagResolverCatalog>();

            new NotesDbContextMigration().Migrate(context, logger);
            new ApplicationDataSeed(context, catalog).SeedData();

            return app;
        }
    }
}

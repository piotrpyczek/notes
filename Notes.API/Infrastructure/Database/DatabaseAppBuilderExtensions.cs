
using Notes.Infrastructure;

namespace Notes.API.Infrastructure.Database
{
    public static class DatabaseAppBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            var logger = scope.ServiceProvider.GetService<ILogger<NotesDbContextMigration>>();

            new NotesDbContextMigration().Migrate(context, logger);
            new ApplicationDataSeed(context).SeedData();

            return app;
        }
    }
}

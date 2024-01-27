using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure;
using Polly;
using Polly.Retry;

namespace Notes.API.Infrastructure.Database
{
    public class NotesDbContextMigration
    {
        public void Migrate(NotesDbContext context, ILogger<NotesDbContextMigration> logger)
        {
            var policy = CreatePolicy(logger, nameof(NotesDbContextMigration));
            policy.Execute(() =>
            {
                context.Database.Migrate();
            });
        }

        private RetryPolicy CreatePolicy(ILogger<NotesDbContextMigration> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<Exception>().
                WaitAndRetry(
                    retries,
                    retry => TimeSpan.FromSeconds(5),
                    (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Error migrating database (attempt {retry} of {retries})", prefix, retry, retries);
                    }
                );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Infrastructure;
using Notes.Infrastructure.TagResolver;

namespace Notes.API.Infrastructure.Database
{
    public class ApplicationDataSeed
    {
        private static readonly Guid DevelopmentUserId = Guid.Parse("FB1B849E-2CB3-4ED4-B7FF-FE3A19477FE0");

        private readonly NotesDbContext context;
        private readonly ITagResolverCatalog tagResolverCatalog;

        public ApplicationDataSeed(NotesDbContext context, ITagResolverCatalog tagResolverCatalog)
        {
            this.context = context;
            this.tagResolverCatalog = tagResolverCatalog;
        }

        public void SeedData()
        {
            SeedDefaultUsers();
            SeedTags();
        }

        private void SeedDefaultUsers()
        {
            InsertUserIfNotExists(DevelopmentUserId, "Development");
            context.SaveChanges();
        }

        private void InsertUserIfNotExists(Guid id, string name)
        {
            var user = context.Users
                .FirstOrDefault(x => x.Id == id || x.Name == name);

            if (user != null)
            {
                return;
            }

            user = new User
            {
                Id = id,
                Name = name
            };

            context.Set<User>().Add(user);
        }

        private void SeedTags()
        {
            var existingTags = context.Tags
                .AsNoTracking()
                .ToList();

            foreach (var tagResolver in tagResolverCatalog.TagResolvers
                         .Where(x => existingTags.All(t => t.Name != x.Name)))
            {
                var tag = new Tag
                {
                    Name = tagResolver.Name
                };

                context.Tags.Add(tag);
            }

            context.SaveChanges();
        }
    }
}

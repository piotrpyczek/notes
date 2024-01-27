using Notes.Domain.Entities;
using Notes.Infrastructure;

namespace Notes.API.Infrastructure.Database
{
    public class ApplicationDataSeed
    {
        private static readonly Guid DevelopmentUserId = Guid.Parse("FB1B849E-2CB3-4ED4-B7FF-FE3A19477FE0");

        private readonly NotesDbContext context;

        public ApplicationDataSeed(NotesDbContext context)
        {
            this.context = context;
        }

        public void SeedData()
        {
            SeedDefaultUsers();
        }

        private void SeedDefaultUsers()
        {
            InsertUserIfNotExists(DevelopmentUserId, "Development");
            context.SaveChanges();
        }

        private void InsertUserIfNotExists(Guid id, string name)
        {
            var user = context
                .Set<User>()
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
    }
}

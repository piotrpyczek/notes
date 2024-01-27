using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Infrastructure;

namespace Notes.API.Infrastructure.AppConext
{
    public class AppContextMiddleware : IMiddleware
    {
        private readonly IAppContext appContext;
        private readonly NotesDbContext dbContext;

        public AppContextMiddleware(IAppContext appContext, NotesDbContext dbContext)
        {
            this.appContext = appContext;
            this.dbContext = dbContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userName = context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await GetUserAsync(userName ?? "Development");

            var session = new AppSession
            {
                UserId = user?.Id,
                UserName = user?.Name,
            };

            appContext.Setup(session);

            await next(context);
        }

        private Task<User?> GetUserAsync(string name)
        {
            return dbContext.Users.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}

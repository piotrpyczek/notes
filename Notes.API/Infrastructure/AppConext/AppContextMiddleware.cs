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
            var userId = context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userId != null
                ? await GetUserAsync(Guid.Parse(userId))
                : null;

            var session = new AppSession
            {
                UserId = user?.Id,
                UserName = user?.Name,
            };

            appContext.Setup(session);

            await next(context);
        }

        private Task<User?> GetUserAsync(Guid? userId)
        {
            return dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}

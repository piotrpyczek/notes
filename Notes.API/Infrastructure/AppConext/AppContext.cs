using Notes.Infrastructure;

namespace Notes.API.Infrastructure
{
    public class AppContext : IAppContext
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }

        public void Setup(IAppSession session)
        {
            UserId = session.UserId;
            UserName = session.UserName;
        }
    }
}

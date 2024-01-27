using Notes.Infrastructure;

namespace Notes.API.Infrastructure.AppConext
{
    public class AppSession : IAppSession
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}

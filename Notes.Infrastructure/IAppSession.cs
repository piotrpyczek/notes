namespace Notes.Infrastructure
{
    public interface IAppSession
    {
        Guid? UserId { get; }
        string? UserName { get; }
    }
}

namespace Notes.Infrastructure
{
    public interface IAppContext
    {
        Guid? UserId { get; }
        string? UserName { get; }

        void Setup(IAppSession appSession);
    }
}
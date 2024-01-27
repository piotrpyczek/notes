namespace Notes.Infrastructure.TagResolver
{
    public interface ITagResolver
    {
        string Name { get; }
        bool AppliesTo(string text);
    }
}

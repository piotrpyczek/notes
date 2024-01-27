namespace Notes.Infrastructure.TagResolver
{
    public interface ITagGeneratorService
    {
        string[] GetTags(string text);
    }
}

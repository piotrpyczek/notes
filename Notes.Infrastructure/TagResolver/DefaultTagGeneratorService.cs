namespace Notes.Infrastructure.TagResolver
{
    public class DefaultTagGeneratorService : ITagGeneratorService
    {
        private readonly ITagResolverCatalog catalog;

        public DefaultTagGeneratorService(ITagResolverCatalog catalog)
        {
            this.catalog = catalog;
        }

        public string[] GetTags(string text)
        {
            return catalog.TagResolvers
                .Where(x => x.AppliesTo(text))
                .Select(x => x.Name)
                .ToArray();
        }
    }
}

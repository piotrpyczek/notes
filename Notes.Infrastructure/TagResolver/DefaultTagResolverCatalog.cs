namespace Notes.Infrastructure.TagResolver
{
    public class DefaultTagResolverCatalog : ITagResolverCatalog
    {
        private readonly List<ITagResolver> tagResolvers = new();

        public void Register(ITagResolver resolver)
        {
            if (tagResolvers.Any(x => x.Name.Equals(resolver.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException($"Resolver with name [{resolver.Name}] already exists");
            }

            tagResolvers.Add(resolver);
        }

        public IReadOnlyCollection<ITagResolver> TagResolvers
        {
            get { return tagResolvers.AsReadOnly(); }
        }
    }
}

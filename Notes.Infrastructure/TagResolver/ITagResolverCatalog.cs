namespace Notes.Infrastructure.TagResolver
{
    public interface ITagResolverCatalog
    {
        public void Register(ITagResolver resolver);
        public IReadOnlyCollection<ITagResolver> TagResolvers { get; }
    }
}

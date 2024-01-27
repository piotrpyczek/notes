namespace Notes.Domain.Infrastructure
{
    public interface IEntity<TId>
    {
        TId? Id { get; set; }
    }
}

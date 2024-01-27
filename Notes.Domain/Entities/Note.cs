using Notes.Domain.Infrastructure;

namespace Notes.Domain.Entities
{
    public class Note : Entity
    {
        public string Text { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}

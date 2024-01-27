using Notes.Domain.Infrastructure;

namespace Notes.Domain.Entities
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}

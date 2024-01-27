using Notes.Domain.Infrastructure;

namespace Notes.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}

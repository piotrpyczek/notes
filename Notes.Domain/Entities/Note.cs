using Notes.Domain.Infrastructure;

namespace Notes.Domain.Entities
{
    public class Note : Entity
    {
        public string Text { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}

using Notes.Domain.Infrastructure;

namespace Notes.Domain.Entities
{
    public class Note : Entity
    {
        protected Note()
        {
        }

        public Note(string text)
        {
            Text = !string.IsNullOrWhiteSpace(text) ? text : throw new ArgumentNullException(nameof(text));
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        }

        public string Text { get; private set; }

        public DateTime? CreatedAt { get; private set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}

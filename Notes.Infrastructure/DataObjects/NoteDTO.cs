namespace Notes.Infrastructure.DataObjects
{
    public class NoteDTO
    {
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}

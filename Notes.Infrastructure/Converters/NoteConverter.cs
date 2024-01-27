using Notes.Domain.Entities;
using Notes.Infrastructure.DataObjects;

namespace Notes.Infrastructure.Converters
{
    public static class NoteConverter
    {
        public static NoteDTO ToNoteDTO(this Note note)
        {
            return new NoteDTO
            {
                Id = note.Id,
                Text = note.Text,
                CreatedAt = note.CreatedAt,
                Tags = note.Tags.Select(x => x.ToTagDTO())
            };
        }
    }
}

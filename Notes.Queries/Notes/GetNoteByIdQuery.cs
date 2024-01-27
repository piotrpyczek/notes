using Notes.Domain.Messaging;
using Notes.Infrastructure.DataObjects;

namespace Notes.Queries.Notes
{
    public class GetNoteByIdQuery : IQuery<NoteDTO>
    {
        public GetNoteByIdQuery(Guid? noteId)
        {
            NoteId = noteId;
        }

        public Guid? NoteId { get; }
    }
}

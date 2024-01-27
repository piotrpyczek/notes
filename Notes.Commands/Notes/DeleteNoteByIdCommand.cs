using Notes.Domain.Messaging;

namespace Notes.Commands.Notes
{
    public class DeleteNoteByIdCommand: ICommand
    {
        public DeleteNoteByIdCommand(Guid? noteId)
        {
            NoteId = noteId;
        }

        public Guid? NoteId { get; }
    }
}

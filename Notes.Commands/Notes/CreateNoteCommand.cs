using Notes.Domain.Messaging;

namespace Notes.Commands.Notes
{
    public class CreateNoteCommand : ICommand<Guid?>
    {
        public string Text { get; set; }
    }
}

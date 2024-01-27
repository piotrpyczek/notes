using Notes.Domain.Entities;
using Notes.Domain.Messaging;
using Notes.Infrastructure;

namespace Notes.Commands.Notes.Handlers
{
    public class CreateNoteCommandHandler : CommandHandler<CreateNoteCommand, Guid?>
    {
        private readonly IAppContext appContext;
        private readonly NotesDbContext dbContext;

        public CreateNoteCommandHandler(IAppContext appContext, NotesDbContext dbContext)
        {
            this.appContext = appContext;
            this.dbContext = dbContext;
        }

        public override async Task<Guid?> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                Text = command.Text,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                UserId = appContext.UserId
            };

            dbContext.Notes.Add(note);
            await dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}

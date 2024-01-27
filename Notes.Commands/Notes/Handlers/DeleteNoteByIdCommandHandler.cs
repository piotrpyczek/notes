using Notes.Domain.Messaging;
using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure;

namespace Notes.Commands.Notes.Handlers
{
    public class DeleteNoteByIdCommandHandler : CommandHandler<DeleteNoteByIdCommand>
    {
        private readonly IAppContext appContext;
        private readonly NotesDbContext dbContext;

        public DeleteNoteByIdCommandHandler(IAppContext appContext, NotesDbContext dbContext)
        {
            this.appContext = appContext;
            this.dbContext = dbContext;
        }

        public override async Task Handle(DeleteNoteByIdCommand command, CancellationToken cancellationToken)
        {
            var note = await dbContext.Notes
                .Where(x => x.Id == command.NoteId && x.UserId == appContext.UserId)
                .SingleOrDefaultAsync(cancellationToken);

            if (note == null)
            {
                throw new ApplicationException("Note not found");
            }

            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Domain.Messaging;
using Notes.Infrastructure;
using Notes.Infrastructure.TagResolver;

namespace Notes.Commands.Notes.Handlers
{
    public class CreateNoteCommandHandler : CommandHandler<CreateNoteCommand, Guid?>
    {
        private readonly IAppContext appContext;
        private readonly NotesDbContext dbContext;
        private readonly ITagGeneratorService tagGeneratorService;

        public CreateNoteCommandHandler(IAppContext appContext, NotesDbContext dbContext, ITagGeneratorService tagGeneratorService)
        {
            this.appContext = appContext;
            this.dbContext = dbContext;
            this.tagGeneratorService = tagGeneratorService;
        }

        public override async Task<Guid?> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            var tagNames = tagGeneratorService.GetTags(command.Text);

            var tags = await dbContext.Tags
                .Where(x => tagNames.Contains(x.Name))
                .ToListAsync(cancellationToken);

            var note = new Note(command.Text)
            {
                UserId = appContext.UserId,
                Tags = tags
            };

            dbContext.Notes.Add(note);
            await dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}

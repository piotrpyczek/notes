using Notes.Domain.Messaging;
using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure;
using Notes.Infrastructure.Converters;
using Notes.Infrastructure.DataObjects;

namespace Notes.Queries.Notes.Handlers
{
    public class GetNotesQueryHandler : QueryHandler<GetNotesQuery, IEnumerable<NoteDTO>>
    {
        private readonly IAppContext appContext;
        private readonly NotesDbContext dbContext;

        public GetNotesQueryHandler(IAppContext appContext, NotesDbContext dbContext)
        {
            this.appContext = appContext;
            this.dbContext = dbContext;
        }

        public override async Task<IEnumerable<NoteDTO>> Handle(GetNotesQuery query, CancellationToken cancellationToken)
        {
            var notes = await dbContext.Notes
                .AsNoTracking()
                .Include(x => x.Tags)
                .Where(x => x.User.Id == appContext.UserId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return notes.Select(note => note.ToNoteDTO());
        }
    }
}

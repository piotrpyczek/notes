using Microsoft.EntityFrameworkCore;
using Notes.Domain.Messaging;
using Notes.Infrastructure;
using Notes.Infrastructure.Converters;
using Notes.Infrastructure.DataObjects;

namespace Notes.Queries.Notes.Handlers
{
    public class GetNoteByIdQueryHandler : QueryHandler<GetNoteByIdQuery, NoteDTO>
    {
        private readonly IAppContext appContext;
        private readonly NotesDbContext dbContext;

        public GetNoteByIdQueryHandler(IAppContext appContext, NotesDbContext dbContext)
        {
            this.appContext = appContext;
            this.dbContext = dbContext;
        }

        public override async Task<NoteDTO> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
        {
            var note = await dbContext.Notes
                .AsNoTracking()
                .Include(x => x.Tags)
                .Where(x => x.Id == query.NoteId && x.UserId == appContext.UserId)
                .SingleOrDefaultAsync(cancellationToken);

            if (note == null)
            {
                throw new ApplicationException("Note not found");
            }

            return note.ToNoteDTO();
        }
    }
}

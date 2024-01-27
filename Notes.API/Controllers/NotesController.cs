using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notes.Commands.Notes;
using Notes.Infrastructure.DataObjects;
using Notes.Queries.Notes;


namespace Notes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMediator mediator;

        public NotesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<NoteDTO>> GetNotesAsync(CancellationToken cancellationToken)
        {
            return mediator.Send(new GetNotesQuery(), cancellationToken);
        }

        [HttpGet("{noteId}")]
        public Task<NoteDTO> GetNoteAsync(Guid noteId, CancellationToken cancellationToken)
        {
            return mediator.Send(new GetNoteByIdQuery(noteId), cancellationToken);
        }

        [HttpPost]
        public async Task<NoteDTO> CreateNoteAsync([FromBody] CreateNoteCommand command)
        {
            var noteId = await mediator.Send(command);
            return await mediator.Send(new GetNoteByIdQuery(noteId));
        }

        [HttpDelete("{noteId}")]
        public Task DeleteNoteAsync(Guid noteId)
        {
            return mediator.Send(new DeleteNoteByIdCommand(noteId));
        }
    }
}

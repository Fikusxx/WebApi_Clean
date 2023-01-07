using Clean.Application.DTO.NoteDTO;
using MediatR;

namespace Clean.Application.Notes.Queries.GetNoteList;

public class GetNoteListQuery : IRequest<List<NoteLookupDTO>>
{
    public Guid UserId { get; set; }
}

using Clean.Application.DTO.NoteDTO;
using MediatR;

namespace Clean.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQuery : IRequest<NoteDetailsDTO>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}

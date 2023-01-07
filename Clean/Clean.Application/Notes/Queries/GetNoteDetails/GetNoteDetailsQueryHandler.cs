using AutoMapper;
using Clean.Application.Common.Exceptions;
using Clean.Application.DTO.NoteDTO;
using Clean.Application.Interfaces;
using Clean.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsDTO>
{
    private readonly INotesDbContext notesDbContext;
    private readonly IMapper mapper;

    public GetNoteDetailsQueryHandler(INotesDbContext notesDbContext, IMapper mapper)
    {
        this.notesDbContext = notesDbContext;
        this.mapper = mapper;
    }

    public async Task<NoteDetailsDTO> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
    {
        var note = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (note == null || note.UserId != request.UserId)
            throw new NotFoundException(nameof(Note), request.Id);

        var noteDetailsDTO = mapper.Map<NoteDetailsDTO>(note);

        return noteDetailsDTO;
    }
}

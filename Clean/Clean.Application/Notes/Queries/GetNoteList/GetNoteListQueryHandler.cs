using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clean.Application.DTO.NoteDTO;
using Clean.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, List<NoteLookupDTO>>
{
    private readonly INotesDbContext notesDbContext;
    private readonly IMapper mapper;

    public GetNoteListQueryHandler(INotesDbContext notesDbContext, IMapper mapper)
    {
        this.notesDbContext = notesDbContext;
        this.mapper = mapper;
    }

    public async Task<List<NoteLookupDTO>> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        var noteList = await notesDbContext.Notes.Where(x => x.UserId == request.UserId)
            //.ProjectTo<NoteLookupDTO>(mapper.ConfigurationProvider)
            .ToListAsync();

        var noteListDTO = mapper.Map<List<NoteLookupDTO>>(noteList);

        return noteListDTO;
    }
}

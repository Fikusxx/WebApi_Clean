using AutoMapper;
using Clean.Application.Common.Mappings;
using Clean.Application.Notes.Commands.UpdateNote;

namespace Clean.Application.DTO.NoteDTO;


public class UpdateNoteDTO : IMapWith<UpdateNoteCommand>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateNoteDTO, UpdateNoteCommand>().ReverseMap();
    }
}

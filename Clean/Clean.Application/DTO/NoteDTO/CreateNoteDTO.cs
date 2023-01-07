using AutoMapper;
using Clean.Application.Common.Mappings;
using Clean.Application.Notes.Commands.CreateNote;
using System.Security.AccessControl;

namespace Clean.Application.DTO.NoteDTO;

public class CreateNoteDTO : IMapWith<CreateNoteCommand>
{
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNoteDTO, CreateNoteCommand>().ReverseMap();
    }
}

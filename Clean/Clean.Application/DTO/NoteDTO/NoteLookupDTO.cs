using AutoMapper;
using Clean.Application.Common.Mappings;
using Clean.Domain;

namespace Clean.Application.DTO.NoteDTO;


public class NoteLookupDTO : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<NoteLookupDTO, Note>().ReverseMap();
    }
}

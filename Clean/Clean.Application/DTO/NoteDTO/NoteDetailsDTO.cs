using AutoMapper;
using Clean.Application.Common.Mappings;
using Clean.Domain;

namespace Clean.Application.DTO.NoteDTO;

public class NoteDetailsDTO : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<NoteDetailsDTO, Note>().ReverseMap();
    }
}

using AutoMapper;
using Clean.Application.DTO.NoteDTO;
using Clean.Application.Notes.Commands.CreateNote;
using Clean.Application.Notes.Commands.DeleteNote;
using Clean.Application.Notes.Commands.UpdateNote;
using Clean.Application.Notes.Queries.GetNoteDetails;
using Clean.Application.Notes.Queries.GetNoteList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.WebApi.Controllers;


[ApiVersion("1.0")]
[ApiVersion("2.0")]
//[ApiVersionNeutral]
[Route("api/[controller]")]
//[Authorize]
public class NoteController : BaseController
{
    private readonly IMapper mapper;

    public NoteController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    /// <summary>
    /// Get list of all notes.
    /// </summary>
    /// <remarks>
    /// Extra info.
    /// </remarks>
    /// <returns> </returns>
    /// <response code="200"> Code 200 description </response>
    /// <response code="401"> Code 401 description </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<NoteLookupDTO>>> GetAll()
    {
        var query = new GetNoteListQuery()
        {
            UserId = UserId
        };

        var noteListDTO = await Mediator.Send(query);

        return Ok(noteListDTO);
    }

    /// <summary>
    /// Get list of all notes.
    /// </summary>
    /// <remarks>
    /// Extra info.
    /// </remarks>
    /// <returns> </returns>
    /// <param name="id">Id of the note </param>
    /// <response code="200"> Code 200 description </response>
    /// <response code="401"> Code 401 description </response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<NoteDetailsDTO>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery()
        {
            Id = id,
            UserId = UserId
        };

        var noteDetailsDTO = await Mediator.Send(query);

        return Ok(noteDetailsDTO);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateNoteDTO createNoteDTO)
    {
        var command = mapper.Map<CreateNoteCommand>(createNoteDTO);
        command.UserId = UserId;

        var guid = await Mediator.Send(command);

        return Ok(guid);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateNoteDTO updateNoteDTO)
    {
        var command = mapper.Map<UpdateNoteCommand>(updateNoteDTO);
        command.UserId = UserId;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand()
        {
            Id = id,
            UserId = UserId
        };

        await Mediator.Send(command);

        return NoContent();
    }
}


using Clean.Application.Common.Exceptions;
using Clean.Application.Interfaces;
using Clean.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly INotesDbContext notesDbContext;

    public UpdateNoteCommandHandler(INotesDbContext notesDbContext)
    {
        this.notesDbContext = notesDbContext;
    }

    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (note == null || note.UserId != request.UserId)
            throw new NotFoundException(nameof(Note), request.Id);

        note.UserId = request.UserId;
        note.Title = request.Title;
        note.Details = request.Details;
        note.EditDate = DateTime.Now;

        notesDbContext.Notes.Update(note);
        await notesDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

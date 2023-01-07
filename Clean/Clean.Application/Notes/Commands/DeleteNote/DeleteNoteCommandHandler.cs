using Clean.Application.Common.Exceptions;
using Clean.Application.Interfaces;
using Clean.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDbContext notesDbContext;

    public DeleteNoteCommandHandler(INotesDbContext notesDbContext)
    {
        this.notesDbContext = notesDbContext;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (note == null || note.UserId != request.UserId)
            throw new NotFoundException(nameof(Note), request.Id);

        notesDbContext.Notes.Remove(note);
        await notesDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

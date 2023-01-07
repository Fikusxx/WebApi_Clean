using Clean.Application.Interfaces;
using Clean.Domain;
using MediatR;

namespace Clean.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INotesDbContext notesDbContext;

    public CreateNoteCommandHandler(INotesDbContext notesDbContext)
    {
        this.notesDbContext = notesDbContext;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note()
        {
            UserId = request.UserId,
            Title = request.Title,
            Details = request.Details,
            CreationDate = DateTime.Now,
            EditDate = null,
        };

        await notesDbContext.Notes.AddAsync(note, cancellationToken);
        await notesDbContext.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}

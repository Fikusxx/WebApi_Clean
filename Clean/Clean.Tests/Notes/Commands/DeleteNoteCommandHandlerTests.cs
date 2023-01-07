using Clean.Application.Common.Exceptions;
using Clean.Application.Notes.Commands.DeleteNote;
using Clean.Tests.Common;

namespace Clean.Tests.Notes.Commands;

public class DeleteNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);
        var deleteNoteCommand = new DeleteNoteCommand()
        {
            Id = NotesContextFactory.NoteIdForDelete,
            UserId = NotesContextFactory.UserAId
        };

        // Act
        await handler.Handle(deleteNoteCommand, CancellationToken.None);
        var note = Context.Notes.FirstOrDefault(x => x.Id == NotesContextFactory.NoteIdForDelete);

        // Assert
        Assert.Null(note);
    }

    [Fact]
    public async Task DeleteNotCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);
        var deleteNoteCommand = new DeleteNoteCommand()
        {
            Id = Guid.NewGuid(),
            UserId = NotesContextFactory.UserAId
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
         {
             await handler.Handle(deleteNoteCommand, CancellationToken.None);
         });
    }

    [Fact]
    public async Task DeleteNotCommandHandler_FailOnWrongUserId()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);
        var deleteNoteCommand = new DeleteNoteCommand()
        {
            Id = NotesContextFactory.NoteIdForDelete,
            UserId = Guid.NewGuid()
        };

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(deleteNoteCommand, CancellationToken.None);
        });
    }
}

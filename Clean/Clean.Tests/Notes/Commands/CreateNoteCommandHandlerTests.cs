using Clean.Application.Notes.Commands.CreateNote;
using Clean.Tests.Common;


namespace Clean.Tests.Notes.Commands;

public class CreateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new CreateNoteCommandHandler(Context);
        var noteName = "note name";
        var noteDetails = "note details";
        var createNoteCommand = new CreateNoteCommand()
        {
            Title = noteName,
            Details = noteDetails,
            UserId = NotesContextFactory.UserAId
        };

        // Act
        var noteId = await handler.Handle(createNoteCommand, CancellationToken.None);
        var note = Context.Notes.FirstOrDefault(x => x.Id == noteId
        && x.Title == noteName && x.Details == noteDetails);

        // Assert
        Assert.NotNull(note);
    }
}

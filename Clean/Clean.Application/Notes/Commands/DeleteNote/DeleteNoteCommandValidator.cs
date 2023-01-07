using FluentValidation;


namespace Clean.Application.Notes.Commands.DeleteNote;


public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
	public DeleteNoteCommandValidator()
	{
        RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
    }
}

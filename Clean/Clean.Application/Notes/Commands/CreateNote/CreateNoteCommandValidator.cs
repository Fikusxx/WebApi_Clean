using FluentValidation;

namespace Clean.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
	public CreateNoteCommandValidator()
	{
		RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
		RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} is required.");
	}
}

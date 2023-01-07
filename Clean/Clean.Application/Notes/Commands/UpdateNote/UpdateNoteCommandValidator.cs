using Clean.Application.Notes.Commands.CreateNote;
using FluentValidation;


namespace Clean.Application.Notes.Commands.UpdateNote;


public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} is required.");
    }
}

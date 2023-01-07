using FluentValidation;


namespace Clean.Application.Notes.Queries.GetNoteList;


public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
{
	public GetNoteListQueryValidator()
	{
        RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
    }
}

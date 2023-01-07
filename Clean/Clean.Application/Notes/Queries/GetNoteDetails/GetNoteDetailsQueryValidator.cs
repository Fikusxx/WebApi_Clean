using FluentValidation;


namespace Clean.Application.Notes.Queries.GetNoteDetails;


public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
	public GetNoteDetailsQueryValidator()
	{
		RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
		RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
	}
}

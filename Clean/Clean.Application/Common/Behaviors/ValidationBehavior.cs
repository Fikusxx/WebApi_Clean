using FluentValidation;
using MediatR;


namespace Clean.Application.Common.Behaviors;


public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = validators.Select(validator => validator.Validate(context))
                                 .SelectMany(result => result.Errors)
                                 .Where(failure => failure != null)
                                 .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next.Invoke();
    }
}

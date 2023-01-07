using MediatR;
using Serilog;

namespace Clean.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = Guid.NewGuid();
        //var userId = _currentUserService.UserId;

        Log.Information("Notes Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        var response = await next.Invoke();

        return response;
    }
}

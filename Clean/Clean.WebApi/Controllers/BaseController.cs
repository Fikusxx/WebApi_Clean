using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clean.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator mediator;
    protected IMediator Mediator
    {
        get
        {
            return mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        }
    }

    internal Guid UserId
    {
        get
        {
            if (User.Identity.IsAuthenticated == false)
                return Guid.Empty;

            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }

}

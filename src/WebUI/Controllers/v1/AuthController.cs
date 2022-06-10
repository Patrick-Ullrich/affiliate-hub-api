using AffiliateHub.Application.Users.Commands.LoginUser;
using AffiliateHub.Application.Users.Commands.RegisterUser;
using AffiliateHub.Application.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateHub.WebUI.Controllers;

public class AuthController : ApiControllerBase
{

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AuthData>> Register(RegisterUserCommand command)
    {
        return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command));
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthData>> Login(LoginUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
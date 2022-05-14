using AffiliateHub.Application.Users.Commands.LoginUser;
using AffiliateHub.Application.Users.Commands.RegisterUser;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Queries.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateHub.WebUI.Controllers;

public class AuthController : ApiControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<AuthData>> Register(RegisterUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthData>> Login(LoginUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
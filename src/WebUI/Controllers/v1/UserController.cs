using AffiliateHub.Application.Users.Commands.UpdatePassword;
using AffiliateHub.Application.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace AffiliateHub.WebUI.Controllers;

[Authorize]
public class UserController : ApiControllerBase
{

    [HttpPatch("/password")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> UpdatePassword([FromBody] UpdatePasswordCommand command)
    {
        return await Mediator.Send(command);
    }
}
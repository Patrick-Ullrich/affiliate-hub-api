using System.Security.Claims;
using AffiliateHub.Application.Users.Commands.LoginUser;
using AffiliateHub.Application.Users.Commands.RegisterUser;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Queries.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateHub.WebUI.Controllers;

[Authorize]
public class TestController : ApiControllerBase
{

    [HttpGet]
    public ActionResult<string> Hello()
    {
        return "Hello";
    }
}
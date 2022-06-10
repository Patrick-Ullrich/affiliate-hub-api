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
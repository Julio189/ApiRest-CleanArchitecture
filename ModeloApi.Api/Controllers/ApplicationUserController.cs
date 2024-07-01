using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Infra.Data.Authentication.Interfaces;

namespace ModeloApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicationUserController : ControllerBase
{
    private readonly IApplicationUserService _userService;

    public ApplicationUserController(IApplicationUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var result = await _userService.GetAllApplicationUsersAsync();

        return Ok(result.Data);
    }
}

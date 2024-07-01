using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Infra.Data.Authentication.AuthDtos;
using ModeloApi.Infra.Data.Authentication.Interfaces;

namespace ModeloApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("create-role")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> CreateRole([FromBody] RoleDto roleDto)
    {
        var result = await _authService.CreateRole(roleDto);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [Route("user-to-role")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> AddUserToRole(string email, string roleName)
    {
        var result = await _authService.AddUserToRole(email, roleName);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.Login(loginDto);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result.Data);
    }


    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.Register(registerDto);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [Route("refresh-token")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult> RefreshToken(TokenDto tokenDto)
    {
        var result = await _authService.RefreshToken(tokenDto);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [Route("revoke/{username}")]
    [Authorize]
    public async Task<ActionResult> Revoke(string username)
    {
        var result = await _authService.Revoke(username);

        if(!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }
}

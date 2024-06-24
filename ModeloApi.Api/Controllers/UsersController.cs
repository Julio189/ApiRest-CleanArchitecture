using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Application.DTOs.User;
using ModeloApi.Application.Services.Interfaces;

namespace ModeloApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ICollection<ReadUserDto>>> GetAll()
    {
        var result = await _userService.GetAllUsersAsync();

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadUserDto>> GetById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        if(!result.IsSucess)
            return BadRequest(result); 

        return Ok(result.Data);
    }
  
    [HttpPost]
    public async Task<ActionResult<ReadUserDto>> Post([FromBody] CreateUserDto userDto)
    {
        var result = await _userService.CreateUserAsync(userDto);

        if (!result.IsSucess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateUserDto userDto)
    {
        var result = await _userService.UpdateUserAsync(userDto);

        if(!result.IsSucess)
            return BadRequest(result);  

        return NoContent(); 
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<ActionResult> PutPassword([FromBody] UpdateUserPasswordDto userDto)
    {
        var result = await _userService.UpdatePasswordAsync(userDto);

        if(!result.IsSucess)
            return BadRequest(result); 

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if(!result.IsSucess)
            return BadRequest(result);

        return NoContent();
    }
}

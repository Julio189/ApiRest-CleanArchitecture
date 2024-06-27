//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ModeloApi.Application.DTOs.Login;
//using ModeloApi.Application.Services.Interfaces;

//namespace ModeloApi.Api.Controllers;
//[Route("api/[controller]")]
//[ApiController]
//public class LoginController : ControllerBase
//{
//    private readonly ILoginService _loginService;

//    public LoginController(ILoginService loginService)
//    {
//        _loginService = loginService;
//    }

//    [HttpPost]
//    public async Task<ActionResult<LoginDto>> Login([FromBody] LoginDto loginDto)
//    {
//        var result = await _loginService.GenerateTokenAsync(loginDto);

//        if (!result.IsValid)
//            return BadRequest(result);

//        return Ok(result.Data);
//    }
//}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Application.DTOs.Person;
using ModeloApi.Application.Services.Interfaces;

namespace ModeloApi.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{

    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await _personService.GetAllPeopleAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _personService.GetPersonById(id);

        if (!result.IsSucess)
            return BadRequest(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatePersonDto createPersonDto)
    {
        var result = await _personService.CreatePersonAsync(createPersonDto);
        if(!result.IsSucess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);

    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdatePersonDto updatePersonDto)
    {
        var result = await _personService.UpdatePersonAsync(updatePersonDto);

        if(!result.IsSucess)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personService.DeletePersonAsync(id);

        if(!result.IsSucess)
            return NotFound(result);

        return NoContent();
    }
}

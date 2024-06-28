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
    [Authorize(Policy = "UserOnly")]
    public async Task<ActionResult> GetAll()
    {
        var result = await _personService.GetAllPeopleAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "UserOnly")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _personService.GetPersonById(id);

        if(!result.IsFound)
            return NotFound(result);

        if (!result.IsValid)
            return BadRequest(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatePersonDto createPersonDto)
    {
        var result = await _personService.CreatePersonAsync(createPersonDto);

        if(!result.IsValid)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdatePersonDto updatePersonDto)
    {
        if (id != updatePersonDto.Id)
            return BadRequest("The id in the url does not match the id in the body!");

        var result = await _personService.UpdatePersonAsync(updatePersonDto);

        if (!result.IsFound)
            return NotFound(result);

        if(!result.IsValid)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personService.DeletePersonAsync(id);

        if(!result.IsFound)
            return NotFound(result);

        return NoContent();
    }
}

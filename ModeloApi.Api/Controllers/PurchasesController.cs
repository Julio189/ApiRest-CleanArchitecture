using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Application.DTOs.Purchase;
using ModeloApi.Application.Services.Interfaces;

namespace ModeloApi.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ReadPurchaseDto>>> GetAll()
    {
        var result = await _purchaseService.GetAllPurchasesAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReadPurchaseDto>> GetById(int id)
    {
        var result = await _purchaseService.GetPurchaseByIdAsync(id);

        if(!result.IsSucess)
            return BadRequest(result);

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<ReadPurchaseDto>> Post([FromBody] CreatePurchaseDto purchaseDto)
    {
        var result = await _purchaseService.CreatePurchaseAsync(purchaseDto);

        if (!result.IsSucess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new {id = result.Data.Id}, result.Data);
    }

    [HttpPut]
    public async Task<ActionResult> Put(UpdatePurchaseDto purchaseDto)
    {
        var result = await _purchaseService.UpdatePurchaseAsync(purchaseDto);

        if (!result.IsSucess)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _purchaseService.DeletePurchaseAsync(id);

        if (!result.IsSucess)
            return BadRequest(result);

        return NoContent();
    }
}

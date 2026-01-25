using Microsoft.AspNetCore.Mvc;
using UniConnect.Api.Dtos.GuidanceRequest;
using UniConnect.Api.Services.Interfaces;

namespace UniConnect.Api.Controllers;

[ApiController]
[Route("api/requests")]
public class GuidanceRequestsController : ControllerBase
{
    private readonly IGuidanceRequestService _service;

    public GuidanceRequestsController(IGuidanceRequestService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<RequestResponseDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RequestResponseDto>> GetById(int id)
    {
        var req = await _service.GetByIdAsync(id);
        return req == null ? NotFound() : Ok(req);
    }

    [HttpPost]
    public async Task<ActionResult<RequestResponseDto>> Create(RequestCreateDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<RequestResponseDto>> Update(int id, RequestUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}

using RestApiWithMongoDb.Models;
using RestApiWithMongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace RestApiWithMongoDb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveEntryController : ControllerBase
{
    private readonly LeaveEntryService _leaveEntryService;

    public LeaveEntryController(LeaveEntryService leaveEntryService) =>
        _leaveEntryService = leaveEntryService;

    [HttpGet]
    public async Task<List<LeaveEntry>> Get() =>
        await _leaveEntryService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<LeaveEntry>> Get(string id)
    {
        var leaveEntry = await _leaveEntryService.GetAsync(id);

        if (leaveEntry is null)
        {
            return NotFound();
        }

        return leaveEntry;
    }

    [HttpPost]
    public async Task<IActionResult> Post(LeaveEntry newLeaveEntry)
    {
        await _leaveEntryService.CreateAsync(newLeaveEntry);

        return CreatedAtAction(nameof(Get), new { id = newLeaveEntry.Id }, newLeaveEntry);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, LeaveEntry updatedLeaveEntry)
    {
        var leaveEntry = await _leaveEntryService.GetAsync(id);

        if (leaveEntry is null)
        {
            return NotFound();
        }

        updatedLeaveEntry.Id = leaveEntry.Id;

        await _leaveEntryService.UpdateAsync(id, updatedLeaveEntry);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var leaveEntry = await _leaveEntryService.GetAsync(id);

        if (leaveEntry is null)
        {
            return NotFound();
        }

        await _leaveEntryService.RemoveAsync(id);

        return NoContent();
    }
}
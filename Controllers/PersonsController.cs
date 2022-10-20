using RestApiWithMongoDb.Models;
using RestApiWithMongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace RestApiWithMongoDb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly PersonsService _PersonsService;

    public PersonsController(PersonsService PersonsService) =>
        _PersonsService = PersonsService;

    [HttpGet]
    public async Task<List<Person>> Get() =>
        await _PersonsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Person>> Get(string id)
    {
        var Person = await _PersonsService.GetAsync(id);

        if (Person is null)
        {
            return NotFound();
        }

        return Person;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Person newPerson)
    {
        await _PersonsService.CreateAsync(newPerson);

        return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Person updatedPerson)
    {
        var Person = await _PersonsService.GetAsync(id);

        if (Person is null)
        {
            return NotFound();
        }

        updatedPerson.Id = Person.Id;

        await _PersonsService.UpdateAsync(id, updatedPerson);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Person = await _PersonsService.GetAsync(id);

        if (Person is null)
        {
            return NotFound();
        }

        await _PersonsService.RemoveAsync(id);

        return NoContent();
    }
}
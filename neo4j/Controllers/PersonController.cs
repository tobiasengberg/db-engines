using Microsoft.AspNetCore.Mvc;
using neo4j.Models;
using neo4j.Repositories;

namespace neo4j.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePerson([FromBody] Person person)
    {
        await _personRepository.CreatePersonAsync(person.Name, person.Age);
        return Ok("Person created.");
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _personRepository.GetAllPersonsAsync();
        return Ok(persons);
    }
}
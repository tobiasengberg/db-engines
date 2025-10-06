using cassandra.Models;
using cassandra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cassandra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        await _userRepository.InsertUserAsync(user.Id, user.Name, user.Age);
        return Ok("User created.");
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
}
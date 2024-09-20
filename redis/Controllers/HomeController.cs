using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace redis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IConnectionMultiplexer _redis;

    public HomeController(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    [HttpPost("set")]
    public async Task<IActionResult> SetCacheAsync(string key, string value)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
        {
            return BadRequest("Invalid input. Key and Value must be provided.");
        }
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, value);
        return Ok("Value set in Redis");
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetCacheAsync(string key)
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);
        return Ok(value.ToString());
    }
}
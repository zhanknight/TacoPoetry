using Microsoft.AspNetCore.Mvc;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(ILogger<AuthorsController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public async Task<ActionResult<string[]>> GetAuthors()
    {
        return new string[] { "Author One", "Author Two" };
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<string>> GetAuthor(int id)
    {
        return "Author One";
    }

    [HttpPost]
    public async Task<ActionResult> CreateAuthor([FromBody] string author)
    {
        _logger.LogInformation("Creating new author");

        return CreatedAtRoute("GetAuthor", new { id = 1 }, "Author One");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAuthor(int id, [FromBody] string author)
    {
        _logger.LogInformation("Updating author");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        _logger.LogInformation("Deleting author");
        return NoContent();
    }
}

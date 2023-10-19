using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacoMusings.API.Contexts;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private readonly TacoMusingsContext _context;

    public AuthorsController(ILogger<AuthorsController> logger, TacoMusingsContext context)
    {
        _logger = logger;
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<string[]>> GetAuthors()
    {
        var test = await _context.Author.FirstOrDefaultAsync();
        return new string[] { test.AuthorName, test.AuthorBio };
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

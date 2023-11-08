using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TacoPoetry.API.Models;
using TacoPoetry.API.Services.Interfaces;

namespace TacoPoetry.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting("slidingWindow")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private readonly IAuthorService _service;

    public AuthorsController(ILogger<AuthorsController> logger, IAuthorService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorView>>> GetAuthors()
    {
        var result = await _service.GetAuthors();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorView>> GetAuthor(int id)
    {
        var authorExists = await _service.AuthorExists(id);

        if (!authorExists)
        {
            return NotFound();
        }

        var result = await _service.GetAuthorById(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAuthor([FromBody] AuthorCreate author)
    {
        _logger.LogInformation("Creating new author", author);

        var createdAuthor = await _service.CreateAuthor(author);

        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorId }, createdAuthor);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorCreate author)
    {

        var authorExists = await _service.AuthorExists(id);

        if (!authorExists)
        {
            return NotFound();
        }

        _logger.LogInformation("Updating author", id);

        var updatedAuthor = await _service.UpdateAuthor(id, author);

        return Accepted(updatedAuthor);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        var authorExists = await _service.AuthorExists(id);

        if (!authorExists)
        {
            return NotFound();
        }

        _logger.LogInformation("Deleting author", id);

        var deletedAuthor = await _service.DeleteAuthor(id);

        return Accepted(deletedAuthor);
    }
}

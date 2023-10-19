using Microsoft.AspNetCore.Mvc;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        var result = await _service.GetAuthors();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var result = await _service.GetAuthorById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAuthor([FromBody] Author author)
    {
        _logger.LogInformation("Creating new author", author);

        var createdAuthor = await _service.CreateAuthor(author);

        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorId }, createdAuthor);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAuthor(int id, [FromBody] Author author)
    {
        _logger.LogInformation("Updating author", id);

        var updatedAuthor = await _service.UpdateAuthor(id, author);

        return Accepted(updatedAuthor);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        _logger.LogInformation("Deleting author", id);

        var deletedAuthor = await _service.DeleteAuthor(id);

        return Accepted(deletedAuthor);
    }
}

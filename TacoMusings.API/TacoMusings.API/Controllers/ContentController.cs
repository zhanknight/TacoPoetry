using Microsoft.AspNetCore.Mvc;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController : ControllerBase
{
    private readonly ILogger<ContentController> _logger;
    private readonly IContentService _service;

    public ContentController(ILogger<ContentController> logger, IContentService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContentView>>> GetAllContent()
    {
        var result = await _service.GetContent();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContentView>> GetContentById(int id)
    {
        var contentExists = await _service.ContentExists(id);

        if (!contentExists)
        {
            return NotFound();
        }

        var result = await _service.GetContentById(id);

        return Ok(result);
    }

    [HttpGet("{filtermethod:alpha}")]
    public async Task<ActionResult<IEnumerable<ContentView>>> GetFilteredContent(string filtermethod)
    {
        // not implemeted yet
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> CreateContent([FromBody] Content content)
    {

        _logger.LogInformation("Creating new content", content);

        var createdContent = await _service.CreateContent(content);

        return CreatedAtAction(nameof(GetContentById), new { id = createdContent.ContentId }, createdContent);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateContent(int id, [FromBody] Content content)
    {

        var contentExists = await _service.ContentExists(id);

        if (!contentExists)
        {
            return NotFound();
        }

        _logger.LogInformation("Updating content", id);
        var updatedContent = await _service.UpdateContent(id, content);
        return Accepted(updatedContent);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteContent(int id)
    {

        var contentExists = await _service.ContentExists(id);

        if (!contentExists)
        {
            return NotFound();
        }

        _logger.LogInformation("Deleting content", id);
        var deletedContent = await _service.DeleteContent(id);
        return Accepted(deletedContent);
    }
}

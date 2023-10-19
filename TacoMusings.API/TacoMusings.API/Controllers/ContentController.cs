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
    public async Task<ActionResult<IEnumerable<Content>>> GetAllContent()
    {
        var result = await _service.GetContent();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Content>> GetContentById(int id)
    {
        var result = await _service.GetContentById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{filtermethod:alpha}")]
    public async Task<ActionResult<IEnumerable<Content>>> GetFilteredContent(string filtermethod)
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
        _logger.LogInformation("Updating content", id);
        var updatedContent = await _service.UpdateContent(id, content);
        return Accepted(updatedContent);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteContent(int id)
    {
        _logger.LogInformation("Deleting content", id);
        var deletedContent = await _service.DeleteContent(id);
        return Accepted(deletedContent);
    }
}

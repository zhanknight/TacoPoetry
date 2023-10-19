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
    public async Task<ActionResult<string>> GetContentById(int id)
    {
        return "Content";
    }

    [HttpGet("{filtermethod:alpha}")]
    public async Task<ActionResult<string>> GetFilteredContent(string filtermethod)
    {
        return "Content";
    }

    [HttpPost]
    public async Task<ActionResult> CreateContent([FromBody] string content)
    {
        _logger.LogInformation("Creating new content");

        return CreatedAtRoute("GetContent", new { id = 1 }, "Content");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateContent(int id, [FromBody] string content)
    {
        _logger.LogInformation("Updating content");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteContent(int id)
    {
        _logger.LogInformation("Deleting content");
        return NoContent();
    }
}

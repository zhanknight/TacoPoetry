using Microsoft.AspNetCore.Mvc;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController : ControllerBase
{
    private readonly ILogger<ContentController> _logger;

    public ContentController(ILogger<ContentController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetAllContent()
    {
        return "All Content";
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

using Microsoft.AspNetCore.Mvc;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;

    public TagsController(ILogger<TagsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<string[]>> GetTags()
    {
        return new string[] { "Tag One", "Tag Two" };
    }

    [HttpPost]
    public async Task<ActionResult> AddTag(string tag)
    {
        _logger.LogInformation("Adding new tag");

        return CreatedAtRoute("GetTag", new { id = 1 }, "Tag One");
    }

}

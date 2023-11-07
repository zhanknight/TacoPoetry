using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TacoPoetry.API.Models;
using TacoPoetry.API.Services.Interfaces;

namespace TacoPoetry.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableRateLimiting("slidingWindow")]

public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;
    private readonly ITagService _service;

    public TagsController(ILogger<TagsController> logger, ITagService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        var tags = await _service.GetAllTags();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagsByContentID(int id)
    {
        var result = await _service.GetTagByContentId(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddTag(string tag)
    {
        // not implemented yet
        return NoContent();
    }

    [HttpPatch]
    public async Task<ActionResult> AddTagToContent(int tagId, int contentId)
    {
        // not implemented yet
        return NoContent();
    }

}

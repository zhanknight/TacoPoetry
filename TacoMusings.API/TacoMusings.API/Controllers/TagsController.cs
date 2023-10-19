using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using TacoMusings.API.Models;
using TacoMusings.API.Services.Interfaces;

namespace TacoMusings.API.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<ActionResult<string[]>> GetTags()
    {
        return new string[] { "Tag One", "Tag Two" };
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
        _logger.LogInformation("Adding new tag");

        return CreatedAtRoute("GetTag", new { id = 1 }, "Tag One");
    }

}

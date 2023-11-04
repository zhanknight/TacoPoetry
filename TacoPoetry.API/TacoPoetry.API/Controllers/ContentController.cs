using Microsoft.AspNetCore.Mvc;
using TacoPoetry.API.Models;
using TacoPoetry.API.Services.Interfaces;

namespace TacoPoetry.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController : ControllerBase
{
    private readonly ILogger<ContentController> _logger;
    private readonly IContentService _contentService;
    private readonly IAuthorService _authorService;
    private readonly ITagService _tagService;

    public ContentController(ILogger<ContentController> logger, IContentService service, IAuthorService authorService, ITagService tagService)
    {
        _logger = logger;
        _contentService = service;
        _authorService = authorService;
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContentView>>> GetAllContent()
    {
        var result = await _contentService.GetContent();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContentView>> GetContentById(int id)
    {
        var contentExists = await _contentService.ContentExists(id);

        if (!contentExists)
        {
            return NotFound();
        }

        var result = await _contentService.GetContentById(id);

        return Ok(result);
    }

    [HttpGet("Author/{authorId:int}")]
    public async Task<ActionResult<IEnumerable<ContentView>>> GetContentByAuthor(int authorId)
    {
        var authorExists = await _authorService.AuthorExists(authorId);

        if (!authorExists)
        {
            return NotFound();
        }

        var result = await _contentService.GetContentByAuthor(authorId);

        return Ok(result);
    }

    [HttpGet("Tags/{tagId:int}")]
    public async Task<ActionResult<IEnumerable<ContentView>>> GetContentByTag(int tagId)
    {
        var tagExists = await _tagService.TagExists(tagId);

        if (!tagExists)
        {
            return NotFound();
        }

        var result = await _contentService.GetContentByTag(tagId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateContent([FromBody] ContentCreate content)
    {

        _logger.LogInformation("Creating new content", content);

        var createdContent = await _contentService.CreateContent(content);

        return CreatedAtAction(nameof(GetContentById), new { id = createdContent.ContentId }, createdContent);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateContent(int id, [FromBody] ContentCreate content)
    {

        var contentExists = await _contentService.ContentExists(id);

        if (!contentExists)
        {
            return NotFound();
        }

        _logger.LogInformation("Updating content", id);
        var updatedContent = await _contentService.UpdateContent(id, content);
        return Accepted(updatedContent);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteContent(int id)
    {

        var contentExists = await _contentService.ContentExists(id);

        if (!contentExists)
        {
            return NotFound();
        }

        _logger.LogInformation("Deleting content", id);
        var deletedContent = await _contentService.DeleteContent(id);
        return Accepted(deletedContent);
    }
}

﻿using Microsoft.EntityFrameworkCore;
using TacoPoetry.API.Contexts;
using TacoPoetry.API.Models;
using TacoPoetry.API.Services.Interfaces;

namespace TacoPoetry.API.Services;

public class TagService : ITagService
{
    private readonly ILogger<TagService> _logger;
    private readonly TacoPoetryContext _context;

    public TagService(ILogger<TagService> logger, TacoPoetryContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<bool> TagExists(int id)
    {
        var tag = await _context.Tag.AnyAsync(a => a.TagId == id);
        return tag;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        var tags = await _context.Tag.ToListAsync();
        return tags;
    }

    public async Task<Tag> GetTagById(int id)
    {
        var tag = await _context.Tag.FindAsync(id);
        return await _context.Tag.FindAsync(id);
    }

    public async Task<IEnumerable<Tag>> GetTagByContentId(int contentId)
    {
        var mappedTags = await _context.TagMap.Where(t => t.MappedContentId == contentId).ToListAsync();
        var test = await _context.Tag.Where(t => _context.TagMap.Where(t => t.MappedContentId == contentId).Any(mt => mt.MappedTagId == t.TagId)).ToListAsync();
        return test;
    }

    public async Task<Tag> CreateTag(string tag)
    {
        throw new NotImplementedException();
    }

    public async Task<Tag> UpdateTag(int id, Tag tag)
    {
        _context.Entry(tag).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return tag;
    }

    public async Task DeleteTag(int id)
    {
        var tag = await _context.Tag.FindAsync(id);
        _context.Tag.Remove(tag);
        await _context.SaveChangesAsync();
    }
}

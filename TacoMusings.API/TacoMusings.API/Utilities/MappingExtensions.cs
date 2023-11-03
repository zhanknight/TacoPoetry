using Microsoft.IdentityModel.Tokens;
using TacoMusings.API.Models;

namespace TacoMusings.API.Utilities;

public static class MappingExtensions
{

    public static AuthorView ToViewModel(this Author author)
    {
        return new AuthorView
        {
            AuthorId = author.AuthorId,
            AuthorName = author.AuthorName,
            AuthorBio = author.AuthorBio,
            AuthorPhotoId = author.AuthorPhotoId,
            Content = author.Content.Select(c => c.ToViewModel()).ToList()
        };
    }

    public static Author ToEntityModel(this AuthorCreate author)
    {
        return new Author
        {
            AuthorName = author.AuthorName,
            AuthorBio = author.AuthorBio,
            AuthorPhotoId = author.AuthorPhotoId
        };
    }

    public static ContentView ToViewModel(this Content content)
    {

        string contentTypeAsString = string.Empty;

        switch (content.ContentType)
        {
            case 1:
                contentTypeAsString = "Quote";
                break;
            case 2:
                contentTypeAsString = "Poem";
                break;
            case 3:
                contentTypeAsString = "Haiku";
                break;
            default:
                contentTypeAsString = "Unknown";
                break;
        }

        return new ContentView
        {
            ContentId = content.ContentId,
            ContentAuthorId = content.ContentAuthor,
            ContentType = contentTypeAsString,
            ContentTitle = content.ContentTitle,
            ContentBody = content.ContentBody,
            ContentDate = content.ContentDate.HasValue ? content.ContentDate : DateTime.MinValue,
            ContentSource = content.ContentSource.IsNullOrEmpty() ? "Unknown" : content.ContentSource,
            ContentAuthor = content.ContentAuthorNavigation.AuthorName,
            Tags = content.TagMap.Select(tm => tm.MappedTag).ToList()

        };
    }

    public static Content ToEntityModel(this ContentCreate content)
    {
        return new Content
        {
            ContentAuthor = content.ContentAuthorId,
            ContentType = content.ContentType switch
            {
                "Quote" => 1,
                "Poem" => 2,
                "Haiku" => 3,
                _ => 0
            },
            ContentTitle = content.ContentTitle,
            ContentBody = content.ContentBody,
            ContentDate = content.ContentDate.HasValue ? content.ContentDate : DateTime.MinValue,
            ContentSource = content.ContentSource.IsNullOrEmpty() ? "Unknown" : content.ContentSource
        };
    }
}

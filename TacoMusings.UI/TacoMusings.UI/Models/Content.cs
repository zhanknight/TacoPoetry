
namespace TacoMusings.UI.Models;

public partial class Content
{
    public int ContentId { get; set; }

    public int ContentAuthorId { get; set; }

    public string ContentType { get; set; } = string.Empty;

    public string ContentTitle { get; set; } = string.Empty;

    public string ContentBody { get; set; } = string.Empty;
   
    public DateTime? ContentDate { get; set; }

    public string? ContentSource { get; set; }

    public string ContentAuthor { get; set; } = string.Empty;

    public virtual ICollection<string> Tags { get; set; } = new List<string>();
}
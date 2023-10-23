namespace TacoMusings.UI.Models;


public class Author
{

    public int AuthorId { get; set; }

    public string AuthorName { get; set; }

    public string AuthorBio { get; set; }

    public int AuthorPhotoId { get; set; }

    public virtual ICollection<Content> Content { get; set; } = new List<Content>();
}
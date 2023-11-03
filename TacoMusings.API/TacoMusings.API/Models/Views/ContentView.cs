using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacoMusings.API.Models;

public partial class ContentView
{
    [Key]
    [Column("ContentID")]
    public int ContentId { get; set; }

    public int ContentAuthorId { get; set; }

    public string ContentType { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string ContentTitle { get; set; } = string.Empty;

    [Required]
    [StringLength(1024)]
    [Unicode(false)]
    public string ContentBody { get; set; } = string.Empty;


    [Column(TypeName = "datetime")]
    public DateTime? ContentDate { get; set; }


    [StringLength(255)]
    [Unicode(false)]
    public string? ContentSource { get; set; }

    public string ContentAuthor { get; set; } = string.Empty;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
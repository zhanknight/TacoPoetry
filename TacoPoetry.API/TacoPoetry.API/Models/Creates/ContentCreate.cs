using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacoPoetry.API.Models;

public class ContentCreate
{
    public int ContentAuthorId { get; set; } = 0;
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
    public DateTime? ContentDate { get; set; } = null;
    [StringLength(255)]
    [Unicode(false)]
    public string? ContentSource { get; set; } = string.Empty;
    public ICollection<string> Tags { get; set; } = new List<string>();

}

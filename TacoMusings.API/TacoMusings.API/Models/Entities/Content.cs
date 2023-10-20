using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TacoMusings.API.Models;

public partial class Content
{
    [Key]
    [Column("ContentID")]
    public int ContentId { get; set; }

    public int ContentAuthor { get; set; }

    public int ContentType { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string ContentTitle { get; set; }

    [Required]
    [StringLength(1024)]
    [Unicode(false)]
    public string ContentBody { get; set; }
   

    [Column(TypeName = "datetime")]
    public DateTime? ContentDate { get; set; }


    [StringLength(255)]
    [Unicode(false)]
    public string? ContentSource { get; set; }


    [ForeignKey("ContentAuthor")]
    [InverseProperty("Content")]
    public virtual Author? ContentAuthorNavigation { get; set; } 


    [ForeignKey("ContentType")]
    [InverseProperty("Content")]
    public virtual ContentType? ContentTypeNavigation { get; set; }

    [InverseProperty("MappedContent")]
    public virtual ICollection<TagMap> TagMap { get; set; } = new List<TagMap>();
}
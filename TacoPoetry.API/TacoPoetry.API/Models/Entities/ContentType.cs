using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacoPoetry.API.Models;

public partial class ContentType
{
    [Key]
    [Column("ContentTypeID")]
    public int ContentTypeId { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string ContentTypeName { get; set; }

    [Required]
    [StringLength(500)]
    [Unicode(false)]
    public string ContentTypeDescription { get; set; }

    [JsonIgnore]
    [InverseProperty("ContentTypeNavigation")]
    public virtual ICollection<Content> Content { get; set; } = new List<Content>();
}
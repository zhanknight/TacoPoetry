using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TacoMusings.API.Models;

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

    [InverseProperty("ContentTypeNavigation")]
    public virtual ICollection<Content> Content { get; set; } = new List<Content>();
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacoPoetry.API.Models;

public partial class TagMap
{
    [Key]
    [Column("TagMapID")]
    public int TagMapId { get; set; }

    [Column("MappedContentID")]
    public int MappedContentId { get; set; }

    [Column("MappedTagID")]
    public int MappedTagId { get; set; }

    [ForeignKey("MappedContentId")]
    [InverseProperty("TagMap")]
    public virtual Content MappedContent { get; set; }

    [ForeignKey("MappedTagId")]
    [InverseProperty("TagMap")]
    public virtual Tag MappedTag { get; set; }
}
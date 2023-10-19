using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TacoMusings.API.Models;

public partial class Tag
{
    [Key]
    [Column("TagID")]
    public int TagId { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string TagName { get; set; }

    [InverseProperty("MappedTag")]
    public virtual ICollection<TagMap> TagMap { get; set; } = new List<TagMap>();
}
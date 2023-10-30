using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacoMusings.API.Models;

public partial class TagView
{
    [Key]
    [Column("TagID")]
    public int TagId { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string TagName { get; set; }

    [JsonIgnore]
    [InverseProperty("MappedTag")]
    public virtual ICollection<TagMap> TagMap { get; set; } = new List<TagMap>();
}
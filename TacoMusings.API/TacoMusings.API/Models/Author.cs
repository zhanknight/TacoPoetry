using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TacoMusings.API.Models;

public partial class Author
{
    [Key]
    [Column("AuthorID")]
    public int AuthorId { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string AuthorName { get; set; }

    [Required]
    [StringLength(500)]
    [Unicode(false)]
    public string AuthorBio { get; set; }

    [Column("AuthorPhotoID")]
    public int AuthorPhotoId { get; set; }

    [InverseProperty("ContentAuthorNavigation")]
    public virtual ICollection<Content> Content { get; set; } = new List<Content>();
}
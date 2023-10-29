using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacoMusings.API.Models;

public class AuthorView
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

    public virtual ICollection<ContentView> Content { get; set; } = new List<ContentView>();
}
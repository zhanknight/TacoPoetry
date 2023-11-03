using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TacoPoetry.API.Models;

public class AuthorCreate
{
    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string AuthorName { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string AuthorBio { get; set; } = string.Empty;

    public int AuthorPhotoId { get; set; } = 1;

}

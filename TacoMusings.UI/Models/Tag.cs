using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacoMusings.UI.Models;

public partial class Tag
{

    public int TagId { get; set; }


    public string TagName { get; set; } = string.Empty;


}
using System.ComponentModel.DataAnnotations;

namespace LIcensesPO.Models;

public class Program : BaseEntity
{
    [Required]
    public string Name { get; set; }
}
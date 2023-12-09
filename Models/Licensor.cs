using System.ComponentModel.DataAnnotations;

namespace LIcensesPO.Models;

public class Licensor : BaseEntity
{
    [Required]
    public string Address { get; set; }

    [Required]
    public string Name { get; set; }
}
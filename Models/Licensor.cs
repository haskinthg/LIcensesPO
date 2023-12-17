using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIcensesPO.Models;

public class Licensor: BaseEntity
{
    [Required]
    public string Address { get; set; }

    [Required]
    public string Name { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIcensesPO.Models;

public class Prog: BaseEntity
{
    [Required]
    public string Name { get; set; }
}
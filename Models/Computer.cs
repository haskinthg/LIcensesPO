using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIcensesPO.Models;


public class Computer: BaseEntity
{
    [Required]
    public string Name { get; set; }
}
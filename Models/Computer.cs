using System.ComponentModel.DataAnnotations;

namespace LIcensesPO.Models;


public class Computer : BaseEntity
{
    [Required]
    public string Name { get; set; }
}
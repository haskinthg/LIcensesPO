using System.ComponentModel.DataAnnotations;

namespace LIcensesPO.Models;

public class User : BaseEntity
{
    [Required]
    public string FName { get; set; }

    [Required]
    public string LName { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string URole { get; set; }
}